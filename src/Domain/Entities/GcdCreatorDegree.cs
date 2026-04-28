using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_degree")]
[Index("DegreeId", Name = "idx_gcd_creator_degree_gcd_creator_degree_0e798d2a")]
[Index("CreatorId", Name = "idx_gcd_creator_degree_gcd_creator_degree_3700153c")]
[Index("SchoolId", Name = "idx_gcd_creator_degree_gcd_creator_degree_5fc7164b")]
[Index("Deleted", Name = "idx_gcd_creator_degree_gcd_creator_degree_da602f0b")]
[Index("Modified", Name = "idx_gcd_creator_degree_gcd_creator_degree_modified_36c3bd276c240964_uniq")]
public partial class GcdCreatorDegree
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("degree_year")]
    public int? DegreeYear { get; set; }

    [Column("degree_year_uncertain")]
    public int DegreeYearUncertain { get; set; }

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

    [Column("degree_id")]
    public int DegreeId { get; set; }

    [Column("school_id")]
    public int? SchoolId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdCreatorDegrees")]
    public virtual GcdCreator Creator { get; set; } = null!;

    [ForeignKey("DegreeId")]
    [InverseProperty("GcdCreatorDegrees")]
    public virtual GcdDegree Degree { get; set; } = null!;

    [ForeignKey("SchoolId")]
    [InverseProperty("GcdCreatorDegrees")]
    public virtual GcdSchool? School { get; set; }
}
