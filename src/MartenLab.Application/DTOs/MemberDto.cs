namespace MartenLab.Application.DTOs;

public class MemberDto
{
    public Guid Id { get; init; }
    public string UserId { get; init; } = string.Empty;
    public string Nickname { get; init; } = string.Empty;
    public AccessTokenInfoDto? Token { get; init; }
}