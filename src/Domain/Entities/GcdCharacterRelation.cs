using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_character_relation")]
[Index("FromCharacterId", Name = "idx_gcd_character_relation_gcd_character_relati_from_character_id_f632780f_fk_gcd_chara")]
[Index("RelationTypeId", Name = "idx_gcd_character_relation_gcd_character_relati_relation_type_id_4cfe6f07_fk_gcd_chara")]
[Index("ToCharacterId", Name = "idx_gcd_character_relation_gcd_character_relati_to_character_id_e08d735a_fk_gcd_chara")]
[Index("Modified", Name = "idx_gcd_character_relation_gcd_character_relation_modified_ac98c3e4")]
public partial class GcdCharacterRelation
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

    [Column("from_character_id")]
    public int FromCharacterId { get; set; }

    [Column("relation_type_id")]
    public int RelationTypeId { get; set; }

    [Column("to_character_id")]
    public int ToCharacterId { get; set; }

    [ForeignKey("FromCharacterId")]
    [InverseProperty("GcdCharacterRelationFromCharacters")]
    public virtual GcdCharacter FromCharacter { get; set; } = null!;

    [ForeignKey("RelationTypeId")]
    [InverseProperty("GcdCharacterRelations")]
    public virtual GcdCharacterRelationType RelationType { get; set; } = null!;

    [ForeignKey("ToCharacterId")]
    [InverseProperty("GcdCharacterRelationToCharacters")]
    public virtual GcdCharacter ToCharacter { get; set; } = null!;
}
