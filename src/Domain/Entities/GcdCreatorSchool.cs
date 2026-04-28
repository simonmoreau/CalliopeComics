using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_school")]
[Index("CreatorId", Name = "idx_gcd_creator_school_gcd_creator_school_3700153c")]
[Index("SchoolId", Name = "idx_gcd_creator_school_gcd_creator_school_5fc7164b")]
[Index("Deleted", Name = "idx_gcd_creator_school_gcd_creator_school_da602f0b")]
[Index("Modified", Name = "idx_gcd_creator_school_gcd_creator_school_modified_452d6d27a108110_uniq")]
public partial class GcdCreatorSchool
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("school_year_began")]
    public int? SchoolYearBegan { get; set; }

    [Column("school_year_began_uncertain")]
    public int SchoolYearBeganUncertain { get; set; }

    [Column("school_year_ended")]
    public int? SchoolYearEnded { get; set; }

    [Column("school_year_ended_uncertain")]
    public int SchoolYearEndedUncertain { get; set; }

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

    [Column("school_id")]
    public int SchoolId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdCreatorSchools")]
    public virtual GcdCreator Creator { get; set; } = null!;

    [ForeignKey("SchoolId")]
    [InverseProperty("GcdCreatorSchools")]
    public virtual GcdSchool School { get; set; } = null!;
}
