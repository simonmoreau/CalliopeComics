using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_story_type")]
[Index("Name", IsUnique = true)]
[Index("SortCode", IsUnique = true)]
public partial class GcdStoryType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(50)")]
    public string Name { get; set; } = null!;

    [Column("sort_code")]
    public int SortCode { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<GcdStory> GcdStories { get; set; } = new List<GcdStory>();
}
