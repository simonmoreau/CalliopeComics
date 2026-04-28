using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_universe")]
[Index("Deleted", Name = "idx_gcd_universe_gcd_universe_deleted_ff60af35")]
[Index("Designation", Name = "idx_gcd_universe_gcd_universe_designation_c44a1a75")]
[Index("Modified", Name = "idx_gcd_universe_gcd_universe_modified_c5d3bbeb")]
[Index("Multiverse", Name = "idx_gcd_universe_gcd_universe_multiverse_33bbb4b5")]
[Index("Name", Name = "idx_gcd_universe_gcd_universe_name_6d727a81")]
[Index("VerseId", Name = "idx_gcd_universe_gcd_universe_verse_id_b2ae21fa_fk_gcd_multiverse_id")]
[Index("YearFirstPublished", Name = "idx_gcd_universe_gcd_universe_year_first_published_deedfad5")]
public partial class GcdUniverse
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("created", TypeName = "datetime(6)")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime(6)")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("multiverse", TypeName = "varchar(255)")]
    public string Multiverse { get; set; } = null!;

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("designation", TypeName = "varchar(255)")]
    public string Designation { get; set; } = null!;

    [Column("year_first_published")]
    public int? YearFirstPublished { get; set; }

    [Column("year_first_published_uncertain")]
    public int YearFirstPublishedUncertain { get; set; }

    [Column("description", TypeName = "longtext")]
    public string Description { get; set; } = null!;

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("verse_id")]
    public int? VerseId { get; set; }

    [InverseProperty("Universe")]
    public virtual ICollection<GcdCharacter> GcdCharacters { get; set; } = new List<GcdCharacter>();

    [InverseProperty("Universe")]
    public virtual ICollection<GcdGroupCharacter> GcdGroupCharacters { get; set; } = new List<GcdGroupCharacter>();

    [InverseProperty("Universe")]
    public virtual ICollection<GcdGroup> GcdGroups { get; set; } = new List<GcdGroup>();

    [InverseProperty("Mainstream")]
    public virtual ICollection<GcdMultiverse> GcdMultiverses { get; set; } = new List<GcdMultiverse>();

    [InverseProperty("GroupUniverse")]
    public virtual ICollection<GcdStoryCharacter> GcdStoryCharacterGroupUniverses { get; set; } = new List<GcdStoryCharacter>();

    [InverseProperty("Universe")]
    public virtual ICollection<GcdStoryCharacter> GcdStoryCharacterUniverses { get; set; } = new List<GcdStoryCharacter>();

    [InverseProperty("Universe")]
    public virtual ICollection<GcdStoryUniverse> GcdStoryUniverses { get; set; } = new List<GcdStoryUniverse>();

    [ForeignKey("VerseId")]
    [InverseProperty("GcdUniverses")]
    public virtual GcdMultiverse? Verse { get; set; }
}
