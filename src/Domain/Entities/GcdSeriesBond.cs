using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_series_bond")]
[Index("BondTypeId", Name = "idx_gcd_series_bond_gcd_series_bond_1c107711")]
[Index("OriginIssueId", Name = "idx_gcd_series_bond_gcd_series_bond_22a369b6")]
[Index("Reserved", Name = "idx_gcd_series_bond_gcd_series_bond_3b2a5c11")]
[Index("TargetId", Name = "idx_gcd_series_bond_gcd_series_bond_9358c897")]
[Index("TargetIssueId", Name = "idx_gcd_series_bond_gcd_series_bond_b219039")]
[Index("OriginId", Name = "idx_gcd_series_bond_gcd_series_bond_bd654448")]
public partial class GcdSeriesBond
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("origin_id")]
    public int OriginId { get; set; }

    [Column("target_id")]
    public int TargetId { get; set; }

    [Column("origin_issue_id")]
    public int? OriginIssueId { get; set; }

    [Column("target_issue_id")]
    public int? TargetIssueId { get; set; }

    [Column("bond_type_id")]
    public int BondTypeId { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("reserved")]
    public int Reserved { get; set; }

    [ForeignKey("BondTypeId")]
    [InverseProperty("GcdSeriesBonds")]
    public virtual GcdSeriesBondType BondType { get; set; } = null!;

    [ForeignKey("OriginId")]
    [InverseProperty("GcdSeriesBondOrigins")]
    public virtual GcdSeries Origin { get; set; } = null!;

    [ForeignKey("OriginIssueId")]
    [InverseProperty("GcdSeriesBondOriginIssues")]
    public virtual GcdIssue? OriginIssue { get; set; }

    [ForeignKey("TargetId")]
    [InverseProperty("GcdSeriesBondTargets")]
    public virtual GcdSeries Target { get; set; } = null!;

    [ForeignKey("TargetIssueId")]
    [InverseProperty("GcdSeriesBondTargetIssues")]
    public virtual GcdIssue? TargetIssue { get; set; }
}
