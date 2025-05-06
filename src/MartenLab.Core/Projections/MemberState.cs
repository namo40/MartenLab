namespace MartenLab.Core.Projections;

public class MemberState
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
}