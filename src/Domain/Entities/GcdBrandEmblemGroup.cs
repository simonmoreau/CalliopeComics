using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_brand_emblem_group")]
[Index("BrandId", "BrandgroupId", IsUnique = true)]
[Index("BrandId", Name = "idx_gcd_brand_emblem_group_gcd_brand_emblem_group_74876276")]
[Index("BrandgroupId", Name = "idx_gcd_brand_emblem_group_gcd_brand_emblem_group_9eac909a")]
public partial class GcdBrandEmblemGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("brand_id")]
    public int BrandId { get; set; }

    [Column("brandgroup_id")]
    public int BrandgroupId { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("GcdBrandEmblemGroups")]
    public virtual GcdBrand Brand { get; set; } = null!;

    [ForeignKey("BrandgroupId")]
    [InverseProperty("GcdBrandEmblemGroups")]
    public virtual GcdBrandGroup Brandgroup { get; set; } = null!;
}
