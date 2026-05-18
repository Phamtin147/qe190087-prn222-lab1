using System.ComponentModel.DataAnnotations;

namespace BusinessObjects;

public class AccountMember
{
    public int MemberId { get; set; }

    [StringLength(30)]
    public string? UserName { get; set; }

    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(100)]
    public string EmailAddress { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string MemberPassword { get; set; } = string.Empty;

    public int? MemberRole { get; set; }
}
