using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_indicia_printer")]
[Index("CountryId", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_country_id_7721a445_fk_stddata_country_id")]
[Index("Deleted", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_deleted_7a641d17")]
[Index("Modified", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_modified_778425fb")]
[Index("Name", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_name_2aca0f76")]
[Index("ParentId", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_parent_id_530af9b7_fk_gcd_printer_id")]
[Index("YearBegan", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_year_began_c96ca6c5")]
[Index("YearBeganUncertain", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_year_began_uncertain_0bd7f023")]
[Index("YearEndedUncertain", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_year_ended_uncertain_efe8024d")]
[Index("YearOverallBegan", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_year_overall_began_0ef47eac")]
[Index("YearOverallBeganUncertain", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_year_overall_began_uncertain_c73e8a3a")]
[Index("YearOverallEndedUncertain", Name = "idx_gcd_indicia_printer_gcd_indicia_printer_year_overall_ended_uncertain_1f5b498c")]
public partial class GcdIndiciaPrinter
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

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column("year_began")]
    public int? YearBegan { get; set; }

    [Column("year_ended")]
    public int? YearEnded { get; set; }

    [Column("year_began_uncertain")]
    public int YearBeganUncertain { get; set; }

    [Column("year_ended_uncertain")]
    public int YearEndedUncertain { get; set; }

    [Column("year_overall_began")]
    public int? YearOverallBegan { get; set; }

    [Column("year_overall_ended")]
    public int? YearOverallEnded { get; set; }

    [Column("year_overall_began_uncertain")]
    public int YearOverallBeganUncertain { get; set; }

    [Column("year_overall_ended_uncertain")]
    public int YearOverallEndedUncertain { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("url", TypeName = "varchar(255)")]
    public string Url { get; set; } = null!;

    [Column("issue_count")]
    public int IssueCount { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("parent_id")]
    public int ParentId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("GcdIndiciaPrinters")]
    public virtual StddataCountry Country { get; set; } = null!;

    [InverseProperty("Indiciaprinter")]
    public virtual ICollection<GcdIssueIndiciaPrinter> GcdIssueIndiciaPrinters { get; set; } = new List<GcdIssueIndiciaPrinter>();

    [ForeignKey("ParentId")]
    [InverseProperty("GcdIndiciaPrinters")]
    public virtual GcdPrinter Parent { get; set; } = null!;
}
