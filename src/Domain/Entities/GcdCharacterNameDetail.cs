using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_character_name_detail")]
[Index("CharacterId", Name = "idx_gcd_character_name_detail_gcd_character_name_d_character_id_a0aa1b65_fk_gcd_chara")]
[Index("Deleted", Name = "idx_gcd_character_name_detail_gcd_character_name_detail_deleted_cc14821d")]
[Index("Modified", Name = "idx_gcd_character_name_detail_gcd_character_name_detail_modified_d02579b5")]
[Index("Name", Name = "idx_gcd_character_name_detail_gcd_character_name_detail_name_fb316b3f")]
[Index("SortName", Name = "idx_gcd_character_name_detail_gcd_character_name_detail_sort_name_c86acd29")]
public partial class GcdCharacterNameDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("sort_name", TypeName = "varchar(255)")]
    public string SortName { get; set; } = null!;

    [Column("character_id")]
    public int CharacterId { get; set; }

    [Column("is_official_name")]
    public int IsOfficialName { get; set; }

    [ForeignKey("CharacterId")]
    [InverseProperty("GcdCharacterNameDetails")]
    public virtual GcdCharacter Character { get; set; } = null!;

    [InverseProperty("Character")]
    public virtual ICollection<GcdStoryCharacter> GcdStoryCharacters { get; set; } = new List<GcdStoryCharacter>();
}
