using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("django_content_type")]
[Index("AppLabel", "Model", IsUnique = true)]
public partial class DjangoContentType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("app_label", TypeName = "varchar(100)")]
    public string AppLabel { get; set; } = null!;

    [Column("model", TypeName = "varchar(100)")]
    public string Model { get; set; } = null!;

    [InverseProperty("ContentType")]
    public virtual ICollection<GcdReceivedAward> GcdReceivedAwards { get; set; } = new List<GcdReceivedAward>();

    [InverseProperty("ContentType")]
    public virtual ICollection<TaggitTaggeditem> TaggitTaggeditems { get; set; } = new List<TaggitTaggeditem>();
}
