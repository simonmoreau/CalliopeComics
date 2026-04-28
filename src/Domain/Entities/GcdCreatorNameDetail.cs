using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_name_detail")]
[Index("InScriptId", Name = "idx_gcd_creator_name_detail_gcd_creator_name_det_in_script_id_b7560492_fk_stddata_s")]
[Index("CreatorId", Name = "idx_gcd_creator_name_detail_gcd_creator_name_detail_3700153c")]
[Index("TypeId", Name = "idx_gcd_creator_name_detail_gcd_creator_name_detail_94757cae")]
[Index("Name", Name = "idx_gcd_creator_name_detail_gcd_creator_name_detail_b068931c")]
[Index("Deleted", Name = "idx_gcd_creator_name_detail_gcd_creator_name_detail_da602f0b")]
[Index("FamilyName", Name = "idx_gcd_creator_name_detail_gcd_creator_name_detail_family_name_e81f34f4")]
[Index("GivenName", Name = "idx_gcd_creator_name_detail_gcd_creator_name_detail_given_name_6706c242")]
[Index("Modified", Name = "idx_gcd_creator_name_detail_gcd_creator_name_detail_modified_1bc0d6e170b913fe_uniq")]
[Index("SortName", Name = "idx_gcd_creator_name_detail_gcd_creator_name_detail_sort_name_f7d3caa6")]
public partial class GcdCreatorNameDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("type_id")]
    public int? TypeId { get; set; }

    [Column("sort_name", TypeName = "varchar(255)")]
    public string SortName { get; set; } = null!;

    [Column("is_official_name")]
    public int IsOfficialName { get; set; }

    [Column("in_script_id")]
    public int InScriptId { get; set; }

    [Column("family_name", TypeName = "varchar(255)")]
    public string FamilyName { get; set; } = null!;

    [Column("given_name", TypeName = "varchar(255)")]
    public string GivenName { get; set; } = null!;

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdCreatorNameDetails")]
    public virtual GcdCreator Creator { get; set; } = null!;

    [InverseProperty("Creatornamedetail")]
    public virtual ICollection<GcdCreatorRelationCreatorName> GcdCreatorRelationCreatorNames { get; set; } = new List<GcdCreatorRelationCreatorName>();

    [InverseProperty("Creator")]
    public virtual ICollection<GcdIssueCredit> GcdIssueCredits { get; set; } = new List<GcdIssueCredit>();

    [InverseProperty("Creator")]
    public virtual ICollection<GcdStoryCredit> GcdStoryCredits { get; set; } = new List<GcdStoryCredit>();

    [ForeignKey("InScriptId")]
    [InverseProperty("GcdCreatorNameDetails")]
    public virtual StddataScript InScript { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("GcdCreatorNameDetails")]
    public virtual GcdNameType? Type { get; set; }
}
