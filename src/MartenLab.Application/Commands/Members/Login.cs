using MartenLab.Application.Common;

namespace MartenLab.Application.Commands.Members;

public record Login(string UserId) : ICommand<string>;