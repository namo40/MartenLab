using Marten;
using MartenLab.Application.Commands;
using MartenLab.Application.Common;
using MartenLab.Core.Projections;
using MartenLab.Infrastructure.Extensions;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMartenWithDefaults(builder.Configuration);
builder.Host.UseWolverine(options =>
{
    options.Discovery.IncludeAssembly(typeof(RegisterMember).Assembly);
    options.Policies.AutoApplyTransactions();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/members", async (RegisterMember cmd, IMessageBus bus) =>
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
});

app.MapGet("/members/{id:guid}", async (Guid id, IDocumentSession session) =>
{
    var member = await session.LoadAsync<MemberState>(id);

    if (member == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(member);
});

await app.RunAsync();