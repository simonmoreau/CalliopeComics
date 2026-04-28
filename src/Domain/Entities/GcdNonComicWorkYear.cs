using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_non_comic_work_year")]
[Index("NonComicWorkId", Name = "idx_gcd_non_comic_work_year_gcd_non_comic_work_year_3bb39492")]
public partial class GcdNonComicWorkYear
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("work_year")]
    public int? WorkYear { get; set; }

    [Column("work_year_uncertain")]
    public int WorkYearUncertain { get; set; }

    [Column("non_comic_work_id")]
    public int NonComicWorkId { get; set; }

    [ForeignKey("NonComicWorkId")]
    [InverseProperty("GcdNonComicWorkYears")]
    public virtual GcdCreatorNonComicWork NonComicWork { get; set; } = null!;
}
