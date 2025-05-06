namespace MartenLab.Core.Events;

public record TokenIssued(string UserId, string Token, DateTime ExpiresAt);