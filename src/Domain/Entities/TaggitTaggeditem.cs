using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("taggit_taggeditem")]
[Index("ContentTypeId", "ObjectId", "TagId", IsUnique = true)]
[Index("ContentTypeId", "ObjectId", Name = "idx_taggit_taggeditem_taggit_tagg_content_8fc721_idx")]
[Index("TagId", Name = "idx_taggit_taggeditem_taggit_taggeditem_3747b463")]
[Index("ObjectId", Name = "idx_taggit_taggeditem_taggit_taggeditem_829e37fd")]
[Index("ContentTypeId", Name = "idx_taggit_taggeditem_taggit_taggeditem_e4470c6e")]
public partial class TaggitTaggeditem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tag_id")]
    public int TagId { get; set; }

    [Column("object_id")]
    public int ObjectId { get; set; }

    [Column("content_type_id")]
    public int ContentTypeId { get; set; }

    [ForeignKey("ContentTypeId")]
    [InverseProperty("TaggitTaggeditems")]
    public virtual DjangoContentType ContentType { get; set; } = null!;

    [ForeignKey("TagId")]
    [InverseProperty("TaggitTaggeditems")]
    public virtual TaggitTag Tag { get; set; } = null!;
}
