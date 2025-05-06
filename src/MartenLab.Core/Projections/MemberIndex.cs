using Marten.Events;
using Marten.Events.Aggregation;
using MartenLab.Core.Events;

namespace MartenLab.Core.Projections;

public class MemberIndex
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
}

public class MemberIndexProjection : SingleStreamProjection<MemberIndex>
{
    public MemberIndex Create(IEvent<MemberRegistered> e)
        => new()
        {
            Id = e.StreamKey!,
            UserId = e.Data.UserId,
            Nickname = e.Data.Nickname
        };
}