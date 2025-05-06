using Marten.Events;
using Marten.Events.Aggregation;
using MartenLab.Core.Events;

namespace MartenLab.Core.Projections;

public class MemberProjection : SingleStreamProjection<MemberState>
{
    public MemberState Create(IEvent<MemberRegistered> e)
        => new()
        {
            Id = e.StreamKey!,
            UserId = e.Data.UserId,
            Nickname = e.Data.Nickname
        };

    public void Apply(TokenIssued e, MemberState state)
    {
        state.Token = new AccessTokenInfo
        {
            Token = e.Token,
            ExpiresAt = e.ExpiresAt
        };
    }
}