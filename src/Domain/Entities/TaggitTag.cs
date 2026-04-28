using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("taggit_tag")]
[Index("Slug", IsUnique = true)]
public partial class TaggitTag
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "varchar(100)")]
    public string Name { get; set; } = null!;

    [Column("slug", TypeName = "varchar(100)")]
    public string Slug { get; set; } = null!;

    [InverseProperty("Tag")]
    public virtual ICollection<TaggitTaggeditem> TaggitTaggeditems { get; set; } = new List<TaggitTaggeditem>();
}
