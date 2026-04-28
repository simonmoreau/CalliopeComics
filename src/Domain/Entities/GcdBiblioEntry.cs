using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_biblio_entry")]
public partial class GcdBiblioEntry
{
    [Key]
    [Column("story_ptr_id")]
    public int StoryPtrId { get; set; }

    [Column("page_began")]
    public int? PageBegan { get; set; }

    [Column("page_ended")]
    public int? PageEnded { get; set; }

    [Column("abstract", TypeName = "longtext")]
    public string Abstract { get; set; } = null!;

    [Column("doi", TypeName = "longtext")]
    public string Doi { get; set; } = null!;

    [ForeignKey("StoryPtrId")]
    [InverseProperty("GcdBiblioEntry")]
    public virtual GcdStory StoryPtr { get; set; } = null!;
}
