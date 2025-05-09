﻿using FluentValidation;
using FluentValidation.Results;
using MassTransit;

namespace ToDoApp.Api.Installers.MediatorFilters;

internal class ValidationFilter<TMessage>(
    IEnumerable<IValidator<TMessage>> validators,
    ILogger<ValidationFilter<TMessage>> logger
)
    : IFilter<ConsumeContext<TMessage>>
    where TMessage : class
{
    public void Probe(ProbeContext context)
        => context.CreateFilterScope("validation");

    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        logger.LogDebug("Create Validation filter scope");
        var validationFailures = await ValidateAsync(context.Message, context.CancellationToken);

        if (validationFailures.Length == 0)
        {
            await next.Send(context);
            return;
        }

        throw new ValidationException(validationFailures);
    }

    private async Task<ValidationFailure[]> ValidateAsync(TMessage message, CancellationToken cancellationToken)
    {
        if (!validators.Any())

            return [];
        var context = new ValidationContext<TMessage>(message);

        var validationResults =
            await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        return validationResults
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .ToArray();
    }
}