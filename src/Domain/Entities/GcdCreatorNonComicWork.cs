using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_non_comic_work")]
[Index("WorkRoleId", Name = "idx_gcd_creator_non_comic_work_gcd_creator_non_comic_work_1fd7e1a2")]
[Index("WorkTypeId", Name = "idx_gcd_creator_non_comic_work_gcd_creator_non_comic_work_333f9095")]
[Index("CreatorId", Name = "idx_gcd_creator_non_comic_work_gcd_creator_non_comic_work_3700153c")]
[Index("Deleted", Name = "idx_gcd_creator_non_comic_work_gcd_creator_non_comic_work_da602f0b")]
[Index("Modified", Name = "idx_gcd_creator_non_comic_work_gcd_creator_non_comic_work_modified_1ea7d996ae050445_uniq")]
public partial class GcdCreatorNonComicWork
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("publication_title", TypeName = "varchar(200)")]
    public string PublicationTitle { get; set; } = null!;

    [Column("employer_name", TypeName = "varchar(200)")]
    public string EmployerName { get; set; } = null!;

    [Column("work_title", TypeName = "varchar(255)")]
    public string WorkTitle { get; set; } = null!;

    [Column("work_urls", TypeName = "longtext")]
    public string WorkUrls { get; set; } = null!;

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("work_role_id")]
    public int? WorkRoleId { get; set; }

    [Column("work_type_id")]
    public int WorkTypeId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdCreatorNonComicWorks")]
    public virtual GcdCreator Creator { get; set; } = null!;

    [InverseProperty("NonComicWork")]
    public virtual ICollection<GcdNonComicWorkYear> GcdNonComicWorkYears { get; set; } = new List<GcdNonComicWorkYear>();

    [ForeignKey("WorkRoleId")]
    [InverseProperty("GcdCreatorNonComicWorks")]
    public virtual GcdNonComicWorkRole? WorkRole { get; set; }

    [ForeignKey("WorkTypeId")]
    [InverseProperty("GcdCreatorNonComicWorks")]
    public virtual GcdNonComicWorkType WorkType { get; set; } = null!;
}
