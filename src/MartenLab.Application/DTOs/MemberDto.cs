namespace MartenLab.Application.DTOs;

public class MemberDto
{
    public Guid Id { get; init; }
    public string UserId { get; init; }
    public string Nickname { get; init; }
    public AccessTokenInfoDto? Token { get; init; }
}