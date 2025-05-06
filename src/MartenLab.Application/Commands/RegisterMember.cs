using MartenLab.Application.Common;

namespace MartenLab.Application.Commands;

public record RegisterMember(string UserId, string Nickname) : ICommand<Guid>;
