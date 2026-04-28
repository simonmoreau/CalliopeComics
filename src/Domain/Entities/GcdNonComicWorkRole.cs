using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_non_comic_work_role")]
public partial class GcdNonComicWorkRole
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_name", TypeName = "varchar(200)")]
    public string RoleName { get; set; } = null!;

    [InverseProperty("WorkRole")]
    public virtual ICollection<GcdCreatorNonComicWork> GcdCreatorNonComicWorks { get; set; } = new List<GcdCreatorNonComicWork>();
}
