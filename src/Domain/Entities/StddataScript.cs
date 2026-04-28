using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("stddata_script")]
[Index("Code", IsUnique = true)]
[Index("Number", IsUnique = true)]
public partial class StddataScript
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code", TypeName = "varchar(4)")]
    public string Code { get; set; } = null!;

    [Column("number")]
    public int Number { get; set; }

    [Column("name", TypeName = "varchar(64)")]
    public string Name { get; set; } = null!;

    [InverseProperty("InScript")]
    public virtual ICollection<GcdCreatorNameDetail> GcdCreatorNameDetails { get; set; } = new List<GcdCreatorNameDetail>();
}
