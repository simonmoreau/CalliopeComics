using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_school")]
public partial class GcdSchool
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("school_name", TypeName = "varchar(200)")]
    public string SchoolName { get; set; } = null!;

    [InverseProperty("School")]
    public virtual ICollection<GcdCreatorDegree> GcdCreatorDegrees { get; set; } = new List<GcdCreatorDegree>();

    [InverseProperty("School")]
    public virtual ICollection<GcdCreatorSchool> GcdCreatorSchools { get; set; } = new List<GcdCreatorSchool>();
}
