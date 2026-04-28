using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_printer")]
[Index("CountryId", Name = "idx_gcd_printer_gcd_printer_country_id_889421fa_fk_stddata_country_id")]
[Index("Deleted", Name = "idx_gcd_printer_gcd_printer_deleted_3b0b11ef")]
[Index("IndiciaPrinterCount", Name = "idx_gcd_printer_gcd_printer_indicia_printer_count_c38d8965")]
[Index("Modified", Name = "idx_gcd_printer_gcd_printer_modified_f0a0e321")]
[Index("Name", Name = "idx_gcd_printer_gcd_printer_name_97854c5f")]
[Index("YearBegan", Name = "idx_gcd_printer_gcd_printer_year_began_9471a3eb")]
[Index("YearBeganUncertain", Name = "idx_gcd_printer_gcd_printer_year_began_uncertain_0b919273")]
[Index("YearEndedUncertain", Name = "idx_gcd_printer_gcd_printer_year_ended_uncertain_ff75a9b0")]
[Index("YearOverallBegan", Name = "idx_gcd_printer_gcd_printer_year_overall_began_3ba498cd")]
[Index("YearOverallBeganUncertain", Name = "idx_gcd_printer_gcd_printer_year_overall_began_uncertain_4225255a")]
[Index("YearOverallEndedUncertain", Name = "idx_gcd_printer_gcd_printer_year_overall_ended_uncertain_5e639616")]
public partial class GcdPrinter
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

    [Column("indicia_printer_count")]
    public int IndiciaPrinterCount { get; set; }

    [Column("issue_count")]
    public int IssueCount { get; set; }

    [Column("country_id")]
    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("GcdPrinters")]
    public virtual StddataCountry Country { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<GcdIndiciaPrinter> GcdIndiciaPrinters { get; set; } = new List<GcdIndiciaPrinter>();
}
