using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("stddata_language")]
[Index("Code", IsUnique = true)]
[Index("Name", Name = "idx_stddata_language_language")]
public partial class StddataLanguage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code", TypeName = "varchar(10)")]
    public string Code { get; set; } = null!;

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("native_name", TypeName = "varchar(255)")]
    public string NativeName { get; set; } = null!;

    [InverseProperty("Language")]
    public virtual ICollection<GcdCharacter> GcdCharacters { get; set; } = new List<GcdCharacter>();

    [InverseProperty("Language")]
    public virtual ICollection<GcdFeature> GcdFeatures { get; set; } = new List<GcdFeature>();

    [InverseProperty("Language")]
    public virtual ICollection<GcdGroup> GcdGroups { get; set; } = new List<GcdGroup>();

    [InverseProperty("Language")]
    public virtual ICollection<GcdSeries> GcdSeries { get; set; } = new List<GcdSeries>();
}
