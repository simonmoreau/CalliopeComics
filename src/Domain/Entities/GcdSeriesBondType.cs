using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_series_bond_type")]
[Index("Name", Name = "idx_gcd_series_bond_type_gcd_series_bond_type_52094d6e")]
public partial class GcdSeriesBondType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("description", TypeName = "longtext")]
    public string Description { get; set; } = null!;

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [InverseProperty("BondType")]
    public virtual ICollection<GcdSeriesBond> GcdSeriesBonds { get; set; } = new List<GcdSeriesBond>();
}
