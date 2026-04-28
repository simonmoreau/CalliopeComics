using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_series_publication_type")]
[Index("Name", Name = "idx_gcd_series_publication_type_gcd_series_publication_type_52094d6e")]
public partial class GcdSeriesPublicationType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [InverseProperty("PublicationType")]
    public virtual ICollection<GcdSeries> GcdSeries { get; set; } = new List<GcdSeries>();
}
