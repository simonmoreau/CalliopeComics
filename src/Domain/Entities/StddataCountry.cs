using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("stddata_country")]
[Index("Code", IsUnique = true)]
[Index("Name", Name = "idx_stddata_country_country")]
public partial class StddataCountry
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code", TypeName = "varchar(10)")]
    public string Code { get; set; } = null!;

    [Column("name", TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [InverseProperty("BirthCountry")]
    public virtual ICollection<GcdCreator> GcdCreatorBirthCountries { get; set; } = new List<GcdCreator>();

    [InverseProperty("DeathCountry")]
    public virtual ICollection<GcdCreator> GcdCreatorDeathCountries { get; set; } = new List<GcdCreator>();

    [InverseProperty("Country")]
    public virtual ICollection<GcdIndiciaPrinter> GcdIndiciaPrinters { get; set; } = new List<GcdIndiciaPrinter>();

    [InverseProperty("Country")]
    public virtual ICollection<GcdIndiciaPublisher> GcdIndiciaPublishers { get; set; } = new List<GcdIndiciaPublisher>();

    [InverseProperty("Country")]
    public virtual ICollection<GcdPrinter> GcdPrinters { get; set; } = new List<GcdPrinter>();

    [InverseProperty("Country")]
    public virtual ICollection<GcdPublisher> GcdPublishers { get; set; } = new List<GcdPublisher>();

    [InverseProperty("Country")]
    public virtual ICollection<GcdSeries> GcdSeries { get; set; } = new List<GcdSeries>();
}
