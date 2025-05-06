namespace MartenLab.Core.Projections;

public class AccessTokenInfo
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}

public class MemberState
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public AccessTokenInfo? Token { get; set; }
}