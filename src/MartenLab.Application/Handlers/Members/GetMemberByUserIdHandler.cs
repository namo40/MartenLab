using Marten;
using MartenLab.Application.Common;
using MartenLab.Application.DTOs;
using MartenLab.Application.Queries.Members;
using MartenLab.Core.Projections;

namespace MartenLab.Application.Handlers.Members;

public class GetMemberByUserIdHandler : IQueryHandler<GetMemberByUserId, MemberDto?>
{
    public async Task<MemberDto?> Handle(
        GetMemberByUserId query,
        IQuerySession session,
        CancellationToken cancellationToken)
    {
        var user = await session.Query<MemberState>()
            .FirstOrDefaultAsync(x => x.UserId == query.UserId, cancellationToken);

        if (user is null)
            return null;

        return new()
        {
            Id = user.Id,
            UserId = user.UserId,
            Nickname = user.Nickname,
            Token = user.Token is null
                ? null
                : new()
                {
                    Token = user.Token.Token,
                    ExpiresAt = user.Token.ExpiresAt
                }
        };
    }
}