using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_award")]
[Index("Modified", Name = "idx_gcd_award_gcd_award_9ae73c65")]
[Index("Deleted", Name = "idx_gcd_award_gcd_award_da602f0b")]
public partial class GcdAward
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(200)")]
    public string Name { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [InverseProperty("Award")]
    public virtual ICollection<GcdReceivedAward> GcdReceivedAwards { get; set; } = new List<GcdReceivedAward>();
}
