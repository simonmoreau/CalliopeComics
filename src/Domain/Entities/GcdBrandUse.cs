using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_brand_use")]
[Index("PublisherId", Name = "idx_gcd_brand_use_gcd_brand_use_22dd9c39")]
[Index("EmblemId", Name = "idx_gcd_brand_use_gcd_brand_use_7c3d3954")]
[Index("YearEndedUncertain", Name = "idx_gcd_brand_use_gcd_brand_use_8c53af9d")]
[Index("YearBeganUncertain", Name = "idx_gcd_brand_use_gcd_brand_use_b5b058a2")]
[Index("YearBegan", Name = "idx_gcd_brand_use_gcd_brand_use_d4f3f470")]
[Index("Modified", Name = "idx_gcd_brand_use_gcd_brand_use_modified_2f953692")]
public partial class GcdBrandUse
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("publisher_id")]
    public int PublisherId { get; set; }

    [Column("emblem_id")]
    public int EmblemId { get; set; }

    [Column("year_began")]
    public int? YearBegan { get; set; }

    [Column("year_ended")]
    public int? YearEnded { get; set; }

    [Column("year_began_uncertain")]
    public int YearBeganUncertain { get; set; }

    [Column("year_ended_uncertain")]
    public int YearEndedUncertain { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [ForeignKey("EmblemId")]
    [InverseProperty("GcdBrandUses")]
    public virtual GcdBrand Emblem { get; set; } = null!;

    [ForeignKey("PublisherId")]
    [InverseProperty("GcdBrandUses")]
    public virtual GcdPublisher Publisher { get; set; } = null!;
}
