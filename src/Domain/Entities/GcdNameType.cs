using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_name_type")]
public partial class GcdNameType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("description", TypeName = "longtext")]
    public string Description { get; set; } = null!;

    [Column("type", TypeName = "varchar(50)")]
    public string Type { get; set; } = null!;

    [InverseProperty("Type")]
    public virtual ICollection<GcdCreatorNameDetail> GcdCreatorNameDetails { get; set; } = new List<GcdCreatorNameDetail>();
}
