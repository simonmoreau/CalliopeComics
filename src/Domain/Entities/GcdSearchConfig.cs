using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_search_config")]
public partial class GcdSearchConfig
{
    [Key]
    [Column("k")]
    public string K { get; set; } = null!;

    [Column("v")]
    public int? V { get; set; }
}
