using Marten;
using MartenLab.Application.Commands.Members;
using MartenLab.Application.Common;
using MartenLab.Core.Events;
using MartenLab.Core.Projections;

namespace MartenLab.Application.Handlers.Members;

public class RegisterMemberHandler : ICommandHandler<RegisterMember, string>
{
    public async Task<string> Handle(RegisterMember command, IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var streamId = StreamId<MemberState>.GetStreamId(command.UserId);

        var existing = await session.Events.FetchStreamStateAsync(streamId, cancellationToken);
        if (existing is not null)
            throw new InvalidOperationException("already registered");

        session.Events.StartStream<MemberState>(streamId, new MemberRegistered(command.UserId, command.Nickname));

        await session.SaveChangesAsync(cancellationToken);

        return streamId;
    }
}