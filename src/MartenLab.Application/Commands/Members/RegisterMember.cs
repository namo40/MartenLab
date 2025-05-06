using MartenLab.Application.Common;

namespace MartenLab.Application.Commands.Members;

public record RegisterMember(string UserId, string Nickname) : ICommand<Guid>;
