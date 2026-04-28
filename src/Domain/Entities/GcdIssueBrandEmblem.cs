using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_issue_brand_emblem")]
[Index("IssueId", "BrandId", IsUnique = true)]
[Index("BrandId", Name = "idx_gcd_issue_brand_emblem_gcd_issue_brand_emblem_brand_id_4b9b4e20_fk_gcd_brand_id")]
public partial class GcdIssueBrandEmblem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("issue_id")]
    public int IssueId { get; set; }

    [Column("brand_id")]
    public int BrandId { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("GcdIssueBrandEmblems")]
    public virtual GcdBrand Brand { get; set; } = null!;

    [ForeignKey("IssueId")]
    [InverseProperty("GcdIssueBrandEmblems")]
    public virtual GcdIssue Issue { get; set; } = null!;
}
