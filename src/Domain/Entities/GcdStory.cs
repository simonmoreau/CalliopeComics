using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_story")]
[Index("IssueId", Name = "idx_gcd_story_IssueID")]
[Index("Modified", Name = "idx_gcd_story_Modified")]
[Index("PageCount", Name = "idx_gcd_story_Pg_Cnt")]
[Index("Deleted", Name = "idx_gcd_story_deleted")]
[Index("NoColors", Name = "idx_gcd_story_no_colors")]
[Index("NoEditing", Name = "idx_gcd_story_no_editing")]
[Index("NoInks", Name = "idx_gcd_story_no_inks")]
[Index("NoLetters", Name = "idx_gcd_story_no_letters")]
[Index("NoPencils", Name = "idx_gcd_story_no_pencils")]
[Index("NoScript", Name = "idx_gcd_story_no_script")]
[Index("PageCountUncertain", Name = "idx_gcd_story_page_count_uncertain")]
[Index("TitleInferred", Name = "idx_gcd_story_title_inferred")]
[Index("TypeId", Name = "idx_gcd_story_type_id")]
public partial class GcdStory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title", TypeName = "varchar(255)")]
    public string Title { get; set; } = null!;

    [Column("title_inferred")]
    public int TitleInferred { get; set; }

    [Column("feature", TypeName = "varchar(255)")]
    public string Feature { get; set; } = null!;

    [Column("sequence_number")]
    public int SequenceNumber { get; set; }

    [Column("page_count", TypeName = "decimal(10,3)")]
    public int? PageCount { get; set; }

    [Column("issue_id")]
    public int IssueId { get; set; }

    [Column("script", TypeName = "longtext")]
    public string Script { get; set; } = null!;

    [Column("pencils", TypeName = "longtext")]
    public string Pencils { get; set; } = null!;

    [Column("inks", TypeName = "longtext")]
    public string Inks { get; set; } = null!;

    [Column("colors", TypeName = "longtext")]
    public string Colors { get; set; } = null!;

    [Column("letters", TypeName = "longtext")]
    public string Letters { get; set; } = null!;

    [Column("editing", TypeName = "longtext")]
    public string Editing { get; set; } = null!;

    [Column("genre", TypeName = "varchar(255)")]
    public string Genre { get; set; } = null!;

    [Column("characters", TypeName = "longtext")]
    public string Characters { get; set; } = null!;

    [Column("synopsis", TypeName = "longtext")]
    public string Synopsis { get; set; } = null!;

    [Column("reprint_notes", TypeName = "longtext")]
    public string ReprintNotes { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("no_script")]
    public int NoScript { get; set; }

    [Column("no_pencils")]
    public int NoPencils { get; set; }

    [Column("no_inks")]
    public int NoInks { get; set; }

    [Column("no_colors")]
    public int NoColors { get; set; }

    [Column("no_letters")]
    public int NoLetters { get; set; }

    [Column("no_editing")]
    public int NoEditing { get; set; }

    [Column("page_count_uncertain")]
    public int PageCountUncertain { get; set; }

    [Column("type_id")]
    public int TypeId { get; set; }

    [Column("job_number", TypeName = "varchar(25)")]
    public string JobNumber { get; set; } = null!;

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("first_line", TypeName = "varchar(255)")]
    public string FirstLine { get; set; } = null!;

    [InverseProperty("StoryPtr")]
    public virtual GcdBiblioEntry? GcdBiblioEntry { get; set; }

    [InverseProperty("Story")]
    public virtual ICollection<GcdGroupCharacter> GcdGroupCharacters { get; set; } = new List<GcdGroupCharacter>();

    [InverseProperty("Origin")]
    public virtual ICollection<GcdReprint> GcdReprintOrigins { get; set; } = new List<GcdReprint>();

    [InverseProperty("Target")]
    public virtual ICollection<GcdReprint> GcdReprintTargets { get; set; } = new List<GcdReprint>();

    [InverseProperty("Story")]
    public virtual ICollection<GcdStoryCharacter> GcdStoryCharacters { get; set; } = new List<GcdStoryCharacter>();

    [InverseProperty("Story")]
    public virtual ICollection<GcdStoryCredit> GcdStoryCredits { get; set; } = new List<GcdStoryCredit>();

    [InverseProperty("Story")]
    public virtual ICollection<GcdStoryFeatureLogo> GcdStoryFeatureLogos { get; set; } = new List<GcdStoryFeatureLogo>();

    [InverseProperty("Story")]
    public virtual ICollection<GcdStoryFeatureObject> GcdStoryFeatureObjects { get; set; } = new List<GcdStoryFeatureObject>();

    [InverseProperty("Story")]
    public virtual ICollection<GcdStoryUniverse> GcdStoryUniverses { get; set; } = new List<GcdStoryUniverse>();

    [ForeignKey("IssueId")]
    [InverseProperty("GcdStories")]
    public virtual GcdIssue Issue { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("GcdStories")]
    public virtual GcdStoryType Type { get; set; } = null!;
}
