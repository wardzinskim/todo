using Carter;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Extensions;
using ToDoApp.Application.ToDoTasks.CreateToDoTaskCommand;
using ToDoApp.Application.ToDoTasks.DeleteToDoTaskCommand;
using ToDoApp.Application.ToDoTasks.SetToDoTaskPercentageCompletionCommand;
using ToDoApp.Application.ToDoTasks.UpdateToDoTaskCommand;

namespace ToDoApp.Api.Features;

public class ToDoModule : CarterModule
{
    public ToDoModule() : base("todo")
    {
        WithTags("todo");
        IncludeInOpenApi();
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("", CreateToDoTask).WithName(nameof(CreateToDoTask))
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithOpenApi();

        app.MapPut("{id:guid}", UpdateToDoTask).WithName(nameof(UpdateToDoTask))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithOpenApi();

        app.MapPatch("{id:guid}", SetToDoTaskPercentageCompletion)
            .WithName(nameof(SetToDoTaskPercentageCompletion))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithOpenApi();

        app.MapPost("{id:guid}/mark-as-done", MarkToDoTaskAsDone)
            .WithName(nameof(MarkToDoTaskAsDone))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithOpenApi();

        app.MapDelete("{id:guid}", DeleteToDoTask).WithName(nameof(DeleteToDoTask))
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .WithOpenApi();
    }


    private static async Task<IResult> CreateToDoTask(
        IMediator mediator,
        [FromBody] ToDoTaskRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new CreateToDoTaskCommand(request.Title, request.Description, request.ExpirationDateTime),
            cancellationToken);

        return result.Match(x => Results.CreatedAtRoute("TODO", new {id = x}));
    }

    private static async Task<IResult> UpdateToDoTask(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] ToDoTaskRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new UpdateToDoTaskCommand(id, request.Title, request.Description, request.ExpirationDateTime),
            cancellationToken);

        return result.Match(Results.NoContent);
    }

    private static async Task<IResult> SetToDoTaskPercentageCompletion(
        IMediator mediator,
        [FromRoute] Guid id,
        [FromBody] ToDoTaskSetPercentageCompletionRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new SetToDoTaskPercentageCompletionCommand(id, request.PercentageCompletion),
            cancellationToken);

        return result.Match(Results.NoContent);
    }

    private static async Task<IResult> MarkToDoTaskAsDone(
        IMediator mediator,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new SetToDoTaskPercentageCompletionCommand(id, 100),
            cancellationToken);

        return result.Match(Results.NoContent);
    }

    private static async Task<IResult> DeleteToDoTask(
        IMediator mediator,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new DeleteToDoTaskCommand(id),
            cancellationToken);

        return result.Match(Results.NoContent);
    }
}