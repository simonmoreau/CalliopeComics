using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_relation_creator_name")]
[Index("CreatorrelationId", "CreatornamedetailId", IsUnique = true)]
[Index("CreatornamedetailId", Name = "idx_gcd_creator_relation_creator_name_gcd_creator_relation_creatornamedetail_id_4675e7fc_fk_gcd_creat")]
public partial class GcdCreatorRelationCreatorName
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("creatorrelation_id")]
    public int CreatorrelationId { get; set; }

    [Column("creatornamedetail_id")]
    public int CreatornamedetailId { get; set; }

    [ForeignKey("CreatornamedetailId")]
    [InverseProperty("GcdCreatorRelationCreatorNames")]
    public virtual GcdCreatorNameDetail Creatornamedetail { get; set; } = null!;

    [ForeignKey("CreatorrelationId")]
    [InverseProperty("GcdCreatorRelationCreatorNames")]
    public virtual GcdCreatorRelation Creatorrelation { get; set; } = null!;
}
