using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_group_relation")]
[Index("FromGroupId", Name = "idx_gcd_group_relation_gcd_group_relation_from_group_id_e865b8f6_fk_gcd_group_id")]
[Index("Modified", Name = "idx_gcd_group_relation_gcd_group_relation_modified_19f4c52a")]
[Index("RelationTypeId", Name = "idx_gcd_group_relation_gcd_group_relation_relation_type_id_ab30bd40_fk_gcd_group")]
[Index("ToGroupId", Name = "idx_gcd_group_relation_gcd_group_relation_to_group_id_454ce47f_fk_gcd_group_id")]
public partial class GcdGroupRelation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("from_group_id")]
    public int FromGroupId { get; set; }

    [Column("relation_type_id")]
    public int RelationTypeId { get; set; }

    [Column("to_group_id")]
    public int ToGroupId { get; set; }

    [ForeignKey("FromGroupId")]
    [InverseProperty("GcdGroupRelationFromGroups")]
    public virtual GcdGroup FromGroup { get; set; } = null!;

    [ForeignKey("RelationTypeId")]
    [InverseProperty("GcdGroupRelations")]
    public virtual GcdGroupRelationType RelationType { get; set; } = null!;

    [ForeignKey("ToGroupId")]
    [InverseProperty("GcdGroupRelationToGroups")]
    public virtual GcdGroup ToGroup { get; set; } = null!;
}
