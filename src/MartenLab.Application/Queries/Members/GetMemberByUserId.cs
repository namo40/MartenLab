using MartenLab.Application.Common;
using MartenLab.Application.DTOs;

namespace MartenLab.Application.Queries.Members;

public record GetMemberByUserId(string UserId) : IQuery<MemberDto?>;