using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("Segid", "Term")]
[Table("gcd_search_idx")]
public partial class GcdSearchIdx
{
    [Key]
    [Column("segid")]
    public int Segid { get; set; }

    [Key]
    [Column("term")]
    public byte[] Term { get; set; } = null!;

    [Column("pgno")]
    public int? Pgno { get; set; }
}
