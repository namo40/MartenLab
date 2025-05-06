using Marten;
using MartenLab.Application.Commands;
using MartenLab.Core.Events;
using MartenLab.Core.Projections;

namespace MartenLab.Application.Handlers;

public static class RegisterMemberHandler
{
    public static async Task<Guid> Handle(RegisterMember command, IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var streamId = Guid.NewGuid();

        session.Events.StartStream<MemberState>(streamId, new MemberRegistered(command.UserId, command.Nickname));

        await session.SaveChangesAsync(cancellationToken);
        
        return streamId;
    }
}