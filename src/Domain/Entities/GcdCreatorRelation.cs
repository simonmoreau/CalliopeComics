using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_relation")]
[Index("FromCreatorId", Name = "idx_gcd_creator_relation_gcd_creator_relation_1d58baf1")]
[Index("RelationTypeId", Name = "idx_gcd_creator_relation_gcd_creator_relation_5f290fb5")]
[Index("ToCreatorId", Name = "idx_gcd_creator_relation_gcd_creator_relation_6821265c")]
[Index("Deleted", Name = "idx_gcd_creator_relation_gcd_creator_relation_da602f0b")]
[Index("Modified", Name = "idx_gcd_creator_relation_gcd_creator_relation_modified_4c3574d61a89568e_uniq")]
public partial class GcdCreatorRelation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("from_creator_id")]
    public int FromCreatorId { get; set; }

    [Column("relation_type_id")]
    public int RelationTypeId { get; set; }

    [Column("to_creator_id")]
    public int ToCreatorId { get; set; }

    [ForeignKey("FromCreatorId")]
    [InverseProperty("GcdCreatorRelationFromCreators")]
    public virtual GcdCreator FromCreator { get; set; } = null!;

    [InverseProperty("Creatorrelation")]
    public virtual ICollection<GcdCreatorRelationCreatorName> GcdCreatorRelationCreatorNames { get; set; } = new List<GcdCreatorRelationCreatorName>();

    [ForeignKey("RelationTypeId")]
    [InverseProperty("GcdCreatorRelations")]
    public virtual GcdRelationType RelationType { get; set; } = null!;

    [ForeignKey("ToCreatorId")]
    [InverseProperty("GcdCreatorRelationToCreators")]
    public virtual GcdCreator ToCreator { get; set; } = null!;
}
