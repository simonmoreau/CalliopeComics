using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator")]
[Index("GcdOfficialName", Name = "idx_gcd_creator_gcd_creator_1ea4c9b1")]
[Index("DeathCountryId", Name = "idx_gcd_creator_gcd_creator_388fb40a")]
[Index("BirthDateId", Name = "idx_gcd_creator_gcd_creator_658bab16")]
[Index("BirthCountryId", Name = "idx_gcd_creator_gcd_creator_84c1a583")]
[Index("DeathDateId", Name = "idx_gcd_creator_gcd_creator_d8bf1a9c")]
[Index("Deleted", Name = "idx_gcd_creator_gcd_creator_da602f0b")]
[Index("Disambiguation", Name = "idx_gcd_creator_gcd_creator_disambiguation_21a5e71c")]
[Index("Modified", Name = "idx_gcd_creator_gcd_creator_modified_5920a4f3544476a0_uniq")]
[Index("SortName", Name = "idx_gcd_creator_gcd_creator_sort_name_80a1e4ff")]
public partial class GcdCreator
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("gcd_official_name", TypeName = "varchar(255)")]
    public string GcdOfficialName { get; set; } = null!;

    [Column("whos_who", TypeName = "varchar(200)")]
    public string? WhosWho { get; set; }

    [Column("birth_country_uncertain")]
    public int BirthCountryUncertain { get; set; }

    [Column("birth_province", TypeName = "varchar(50)")]
    public string BirthProvince { get; set; } = null!;

    [Column("birth_province_uncertain")]
    public int BirthProvinceUncertain { get; set; }

    [Column("birth_city", TypeName = "varchar(200)")]
    public string BirthCity { get; set; } = null!;

    [Column("birth_city_uncertain")]
    public int BirthCityUncertain { get; set; }

    [Column("death_country_uncertain")]
    public int DeathCountryUncertain { get; set; }

    [Column("death_province", TypeName = "varchar(50)")]
    public string DeathProvince { get; set; } = null!;

    [Column("death_province_uncertain")]
    public int DeathProvinceUncertain { get; set; }

    [Column("death_city", TypeName = "varchar(200)")]
    public string DeathCity { get; set; } = null!;

    [Column("death_city_uncertain")]
    public int DeathCityUncertain { get; set; }

    [Column("bio", TypeName = "longtext")]
    public string Bio { get; set; } = null!;

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("created", TypeName = "datetime")]
    public DateTime Created { get; set; }

    [Column("modified", TypeName = "datetime")]
    public DateTime Modified { get; set; }

    [Column("deleted")]
    public int Deleted { get; set; }

    [Column("birth_country_id")]
    public int? BirthCountryId { get; set; }

    [Column("birth_date_id")]
    public int? BirthDateId { get; set; }

    [Column("death_country_id")]
    public int? DeathCountryId { get; set; }

    [Column("death_date_id")]
    public int? DeathDateId { get; set; }

    [Column("sort_name", TypeName = "varchar(255)")]
    public string SortName { get; set; } = null!;

    [Column("disambiguation", TypeName = "varchar(255)")]
    public string Disambiguation { get; set; } = null!;

    [ForeignKey("BirthCountryId")]
    [InverseProperty("GcdCreatorBirthCountries")]
    public virtual StddataCountry? BirthCountry { get; set; }

    [ForeignKey("BirthDateId")]
    [InverseProperty("GcdCreatorBirthDates")]
    public virtual StddataDate? BirthDate { get; set; }

    [ForeignKey("DeathCountryId")]
    [InverseProperty("GcdCreatorDeathCountries")]
    public virtual StddataCountry? DeathCountry { get; set; }

    [ForeignKey("DeathDateId")]
    [InverseProperty("GcdCreatorDeathDates")]
    public virtual StddataDate? DeathDate { get; set; }

    [InverseProperty("Creator")]
    public virtual ICollection<GcdCreatorArtInfluence> GcdCreatorArtInfluenceCreators { get; set; } = new List<GcdCreatorArtInfluence>();

    [InverseProperty("InfluenceLink")]
    public virtual ICollection<GcdCreatorArtInfluence> GcdCreatorArtInfluenceInfluenceLinks { get; set; } = new List<GcdCreatorArtInfluence>();

    [InverseProperty("Creator")]
    public virtual ICollection<GcdCreatorDegree> GcdCreatorDegrees { get; set; } = new List<GcdCreatorDegree>();

    [InverseProperty("Creator")]
    public virtual ICollection<GcdCreatorMembership> GcdCreatorMemberships { get; set; } = new List<GcdCreatorMembership>();

    [InverseProperty("Creator")]
    public virtual ICollection<GcdCreatorNameDetail> GcdCreatorNameDetails { get; set; } = new List<GcdCreatorNameDetail>();

    [InverseProperty("Creator")]
    public virtual ICollection<GcdCreatorNonComicWork> GcdCreatorNonComicWorks { get; set; } = new List<GcdCreatorNonComicWork>();

    [InverseProperty("FromCreator")]
    public virtual ICollection<GcdCreatorRelation> GcdCreatorRelationFromCreators { get; set; } = new List<GcdCreatorRelation>();

    [InverseProperty("ToCreator")]
    public virtual ICollection<GcdCreatorRelation> GcdCreatorRelationToCreators { get; set; } = new List<GcdCreatorRelation>();

    [InverseProperty("Creator")]
    public virtual ICollection<GcdCreatorSchool> GcdCreatorSchools { get; set; } = new List<GcdCreatorSchool>();

    [InverseProperty("Creator")]
    public virtual ICollection<GcdCreatorSignature> GcdCreatorSignatures { get; set; } = new List<GcdCreatorSignature>();
}
