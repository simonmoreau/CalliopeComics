using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_character_role")]
[Index("Name", IsUnique = true)]
[Index("SortCode", IsUnique = true)]
public partial class GcdCharacterRole
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(50)")]
    public string Name { get; set; } = null!;

    [Column("sort_code")]
    public int SortCode { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<GcdStoryCharacter> GcdStoryCharacters { get; set; } = new List<GcdStoryCharacter>();
}
