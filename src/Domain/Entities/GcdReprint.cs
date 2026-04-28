using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_reprint")]
[Index("Modified", Name = "idx_gcd_reprint_gcd_reprint_modified_b8966555")]
[Index("OriginIssueId", Name = "idx_gcd_reprint_gcd_reprint_origin_issue_id_35ba9bf2_fk_gcd_issue_id")]
[Index("TargetIssueId", Name = "idx_gcd_reprint_gcd_reprint_target_issue_id_c4c7d013_fk_gcd_issue_id")]
[Index("OriginId", Name = "idx_gcd_reprint_reprint_from")]
[Index("TargetId", Name = "idx_gcd_reprint_reprint_to")]
public partial class GcdReprint
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("origin_id")]
    public int? OriginId { get; set; }

    [Column("target_id")]
    public int? TargetId { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [Column("origin_issue_id")]
    public int OriginIssueId { get; set; }

    [Column("target_issue_id")]
    public int TargetIssueId { get; set; }

    [ForeignKey("OriginId")]
    [InverseProperty("GcdReprintOrigins")]
    public virtual GcdStory? Origin { get; set; }

    [ForeignKey("OriginIssueId")]
    [InverseProperty("GcdReprintOriginIssues")]
    public virtual GcdIssue OriginIssue { get; set; } = null!;

    [ForeignKey("TargetId")]
    [InverseProperty("GcdReprintTargets")]
    public virtual GcdStory? Target { get; set; }

    [ForeignKey("TargetIssueId")]
    [InverseProperty("GcdReprintTargetIssues")]
    public virtual GcdIssue TargetIssue { get; set; } = null!;
}
