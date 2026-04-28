using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_group_name_detail")]
[Index("Deleted", Name = "idx_gcd_group_name_detail_gcd_group_name_detail_deleted_ca292280")]
[Index("GroupId", Name = "idx_gcd_group_name_detail_gcd_group_name_detail_group_id_f6a401d7_fk_gcd_group_id")]
[Index("Modified", Name = "idx_gcd_group_name_detail_gcd_group_name_detail_modified_de08b22d")]
[Index("Name", Name = "idx_gcd_group_name_detail_gcd_group_name_detail_name_d1ffc28a")]
[Index("SortName", Name = "idx_gcd_group_name_detail_gcd_group_name_detail_sort_name_2a643b29")]
public partial class GcdGroupNameDetail
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

    [Column("is_official_name")]
    public int IsOfficialName { get; set; }

    [Column("group_id")]
    public int GroupId { get; set; }

    [InverseProperty("GroupName")]
    public virtual ICollection<GcdGroupCharacter> GcdGroupCharacters { get; set; } = new List<GcdGroupCharacter>();

    [ForeignKey("GroupId")]
    [InverseProperty("GcdGroupNameDetails")]
    public virtual GcdGroup Group { get; set; } = null!;
}
