using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_degree")]
public partial class GcdDegree
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("degree_name", TypeName = "varchar(200)")]
    public string DegreeName { get; set; } = null!;

    [InverseProperty("Degree")]
    public virtual ICollection<GcdCreatorDegree> GcdCreatorDegrees { get; set; } = new List<GcdCreatorDegree>();
}
