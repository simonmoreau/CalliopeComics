using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_group_relation_type")]
public partial class GcdGroupRelationType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type", TypeName = "varchar(50)")]
    public string Type { get; set; } = null!;

    [Column("reverse_type", TypeName = "varchar(50)")]
    public string ReverseType { get; set; } = null!;

    [InverseProperty("RelationType")]
    public virtual ICollection<GcdGroupRelation> GcdGroupRelations { get; set; } = new List<GcdGroupRelation>();
}
