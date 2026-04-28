using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_multiverse")]
[Index("Deleted", Name = "idx_gcd_multiverse_gcd_multiverse_deleted_9d0a1781")]
[Index("MainstreamId", Name = "idx_gcd_multiverse_gcd_multiverse_mainstream_id_9a3651b8_fk_gcd_universe_id")]
[Index("Modified", Name = "idx_gcd_multiverse_gcd_multiverse_modified_835703a1")]
[Index("Name", Name = "idx_gcd_multiverse_gcd_multiverse_name_d34fddb7")]
public partial class GcdMultiverse
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("mainstream_id")]
    public int MainstreamId { get; set; }

    [InverseProperty("Verse")]
    public virtual ICollection<GcdUniverse> GcdUniverses { get; set; } = new List<GcdUniverse>();

    [ForeignKey("MainstreamId")]
    [InverseProperty("GcdMultiverses")]
    public virtual GcdUniverse Mainstream { get; set; } = null!;
}
