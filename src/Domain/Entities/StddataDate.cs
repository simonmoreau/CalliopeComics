using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("stddata_date")]
[Index("Month", Name = "idx_stddata_date_stddata_date_7abe03e0")]
[Index("Year", Name = "idx_stddata_date_stddata_date_b2d73278")]
[Index("Day", Name = "idx_stddata_date_stddata_date_ec444b56")]
public partial class StddataDate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("year", TypeName = "varchar(4)")]
    public string Year { get; set; } = null!;

    [Column("month", TypeName = "varchar(2)")]
    public string Month { get; set; } = null!;

    [Column("day", TypeName = "varchar(2)")]
    public string Day { get; set; } = null!;

    [Column("year_uncertain")]
    public int YearUncertain { get; set; }

    [Column("month_uncertain")]
    public int MonthUncertain { get; set; }

    [Column("day_uncertain")]
    public int DayUncertain { get; set; }

    [InverseProperty("BirthDate")]
    public virtual ICollection<GcdCreator> GcdCreatorBirthDates { get; set; } = new List<GcdCreator>();

    [InverseProperty("DeathDate")]
    public virtual ICollection<GcdCreator> GcdCreatorDeathDates { get; set; } = new List<GcdCreator>();
}
