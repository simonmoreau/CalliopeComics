using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_credit_type")]
[Index("Name", IsUnique = true)]
[Index("SortCode", IsUnique = true)]
public partial class GcdCreditType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(50)")]
    public string Name { get; set; } = null!;

    [Column("sort_code")]
    public int SortCode { get; set; }

    [InverseProperty("CreditType")]
    public virtual ICollection<GcdIssueCredit> GcdIssueCredits { get; set; } = new List<GcdIssueCredit>();

    [InverseProperty("CreditType")]
    public virtual ICollection<GcdStoryCredit> GcdStoryCredits { get; set; } = new List<GcdStoryCredit>();
}
