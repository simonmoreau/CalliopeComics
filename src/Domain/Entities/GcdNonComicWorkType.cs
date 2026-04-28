using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_non_comic_work_type")]
public partial class GcdNonComicWorkType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type", TypeName = "varchar(100)")]
    public string Type { get; set; } = null!;

    [InverseProperty("WorkType")]
    public virtual ICollection<GcdCreatorNonComicWork> GcdCreatorNonComicWorks { get; set; } = new List<GcdCreatorNonComicWork>();
}
