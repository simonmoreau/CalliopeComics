using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_issue_indicia_printer")]
[Index("IssueId", "IndiciaprinterId", IsUnique = true)]
[Index("IndiciaprinterId", Name = "idx_gcd_issue_indicia_printer_gcd_issue_indicia_pr_indiciaprinter_id_c0f3b842_fk_gcd_indic")]
public partial class GcdIssueIndiciaPrinter
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("issue_id")]
    public int IssueId { get; set; }

    [Column("indiciaprinter_id")]
    public int IndiciaprinterId { get; set; }

    [ForeignKey("IndiciaprinterId")]
    [InverseProperty("GcdIssueIndiciaPrinters")]
    public virtual GcdIndiciaPrinter Indiciaprinter { get; set; } = null!;

    [ForeignKey("IssueId")]
    [InverseProperty("GcdIssueIndiciaPrinters")]
    public virtual GcdIssue Issue { get; set; } = null!;
}
