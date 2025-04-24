using Carter;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.Extensions;
using ToDoApp.Application.ToDoTasks.CreateToDoTaskCommand;

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
    }


    private static async Task<IResult> CreateToDoTask(
        IMediator mediator,
        [FromBody] CreateToDoTaskRequest request,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.SendRequest(
            new CreateToDoTaskCommand(request.Title, request.Description, request.ExpirationDateTime),
            cancellationToken);

        return result.Match(x => Results.CreatedAtRoute("TODO", new {id = x}));
    }
}