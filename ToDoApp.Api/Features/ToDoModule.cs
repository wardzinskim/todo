using Carter;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Extensions;
using ToDoApp.Application.ToDoTasks.CreateToDoTaskCommand;
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
            .Produces(StatusCodes.Status200OK)
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

        return result.Match(() => Results.Ok());
    }
}