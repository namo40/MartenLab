using Marten;
using MartenLab.Application.Commands.Members;
using MartenLab.Application.Common;
using MartenLab.Application.Security;
using MartenLab.Core.Events;
using MartenLab.Core.Projections;
using Microsoft.Extensions.Configuration;

namespace MartenLab.Application.Handlers.Members;

public class LoginHandler(IConfiguration configuration) : ICommandHandler<Login, string>
{
    public async Task<string> Handle(Login command, IDocumentSession session, CancellationToken cancellationToken)
    {
        var user = await session.Query<MemberState>()
            .FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellationToken);

        if (user is null)
            throw new InvalidOperationException("not found user");

        var secret = configuration.GetValue<string>("Jwt:Secret")
                     ?? throw new InvalidOperationException("Jwt:Secret");
        var expires = TimeSpan.FromHours(1);

        var token = JwtTokenGenerator.GenerateToken(command.UserId, secret, expires);

        session.Events.Append(user.Id, new TokenIssued(command.UserId, token, DateTime.UtcNow.Add(expires)));

        await session.SaveChangesAsync(cancellationToken);
        return token;
    }
}