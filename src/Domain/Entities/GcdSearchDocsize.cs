using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_search_docsize")]
public partial class GcdSearchDocsize
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("sz")]
    public byte[]? Sz { get; set; }
}
