namespace MartenLab.Application.DTOs;

public class AccessTokenInfoDto
{
    public string Token { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
}