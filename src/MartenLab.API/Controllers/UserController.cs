using MartenLab.Application.Commands.Members;
using MartenLab.Application.Common;
using MartenLab.Application.Queries.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace MartenLab.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMessageBus bus) : ControllerBase
{
    [HttpPost("members")]
    public async Task<IResult> RegisterAsync(RegisterMember cmd)
    {
        try
        {
            var id = await bus.CommandAsync(cmd);

            return Results.Created($"/members/{id}", new { id });
        }
        catch (InvalidOperationException e)
        {
            return Results.BadRequest(new { error = e.Message });
        }
    }

    [Authorize]
    [HttpGet("members")]
    public async Task<IResult> GetMembersAsync()
    {
        var userId = User.Identity?.Name;
        if (userId == null)
            return Results.Unauthorized();
        var member = await bus.QueryAsync(new GetMemberByUserId(userId));
        if (member == null)
            return Results.NotFound();

        return Results.Ok(member);
    }

    [HttpPost("login")]
    public async Task<IResult> LoginAsync(Login cmd)
    {
        try
        {
            var token = await bus.CommandAsync(cmd);

            return Results.Ok(new { token });
        }
        catch (InvalidOperationException e)
        {
            return Results.BadRequest(new { error = e.Message });
        }
    }
}