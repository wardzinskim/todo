using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using ToDoApp.Api.Features;

namespace ToDoApp.Tests.Integration.ToDoTasks;

public class PutToDoTaskTasks(IntegrationTestWebAppFactory application) : BaseIntegrationTest(application)
{
    [Fact]
    public async Task GivenEmptyTitle_Returns400()
    {
        //act
        var response = await _httpClient.PutAsJsonAsync($"/todo/{Guid.NewGuid()}",
            new ToDoTaskRequest(string.Empty, string.Empty, DateTime.Now));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GivenNotExistingId_Returns404()
    {
        //act
        var response = await _httpClient.PutAsJsonAsync($"/todo/{Guid.NewGuid()}",
            new ToDoTaskRequest("test", null, DateTime.Now));

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var problemDetail = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        Assert.NotNull(problemDetail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetail.Status);
        Assert.Equal("to_do_task_not_found", problemDetail.Extensions["code"]!.ToString());
    }

    [Fact]
    public async Task GivenValidData_UpdatesToDoTask()
    {
        //arrange
        Guid id = Guid.NewGuid();
        var todoTask = FakeToDoTaskBuilder.Build(id, "test", null, DateTime.UtcNow);
        _dbContext.ToDoTasks.Add(todoTask);
        await _dbContext.SaveChangesAsync();
        _dbContext.ChangeTracker.Clear();

        var request = new ToDoTaskRequest(
            "test2",
            "test3",
            new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc));

        //act
        var response = await _httpClient.PutAsJsonAsync($"/todo/{id}",
            request);

        //assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        todoTask = await _dbContext.ToDoTasks.FirstOrDefaultAsync(x => x.Id == id);
        Assert.NotNull(todoTask);
        Assert.Equal(request.Title, todoTask.Title);
        Assert.Equal(request.Description, todoTask.Description);
        Assert.Equal(request.ExpirationDateTime, todoTask.ExpirationDateTime);
    }
}