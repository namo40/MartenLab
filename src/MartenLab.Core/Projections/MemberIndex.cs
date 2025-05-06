using Marten.Events.Aggregation;
using MartenLab.Core.Events;

namespace MartenLab.Core.Projections;

public class MemberIndex
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
}

public class MemberIndexProjection : SingleStreamProjection<MemberIndex>
{
    public MemberIndex Create(MemberRegistered e)
        => new()
        {
            Id = Guid.NewGuid(),
            UserId = e.UserId,
            Nickname = e.Nickname
        };
}