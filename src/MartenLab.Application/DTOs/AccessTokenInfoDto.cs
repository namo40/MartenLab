namespace MartenLab.Application.DTOs;

public class AccessTokenInfoDto
{
    public string Token { get; init; }
    public DateTime ExpiresAt { get; init; }
}