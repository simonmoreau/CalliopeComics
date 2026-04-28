using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_search_data")]
public partial class GcdSearchDatum
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("block")]
    public byte[]? Block { get; set; }
}
