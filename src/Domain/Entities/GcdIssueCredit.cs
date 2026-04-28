using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_issue_credit")]
[Index("CreatorId", Name = "idx_gcd_issue_credit_gcd_issue_credit_creator_id_d23239af_fk_gcd_creat")]
[Index("CreditTypeId", Name = "idx_gcd_issue_credit_gcd_issue_credit_credit_type_id_85428160_fk_gcd_credit_type_id")]
[Index("Deleted", Name = "idx_gcd_issue_credit_gcd_issue_credit_deleted_28f591fc")]
[Index("IsCredited", Name = "idx_gcd_issue_credit_gcd_issue_credit_is_credited_448cd70d")]
[Index("IsSourced", Name = "idx_gcd_issue_credit_gcd_issue_credit_is_sourced_f6bb6c2c")]
[Index("IssueId", Name = "idx_gcd_issue_credit_gcd_issue_credit_issue_id_928f51d6_fk_gcd_issue_id")]
[Index("Modified", Name = "idx_gcd_issue_credit_gcd_issue_credit_modified_92c90a4b")]
[Index("Uncertain", Name = "idx_gcd_issue_credit_gcd_issue_credit_uncertain_a122bed6")]
public partial class GcdIssueCredit
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

    [Column("is_credited")]
    public int IsCredited { get; set; }

    [Column("uncertain")]
    public int Uncertain { get; set; }

    [Column("credited_as", TypeName = "varchar(255)")]
    public string CreditedAs { get; set; } = null!;

    [Column("credit_name", TypeName = "varchar(255)")]
    public string CreditName { get; set; } = null!;

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("credit_type_id")]
    public int CreditTypeId { get; set; }

    [Column("issue_id")]
    public int IssueId { get; set; }

    [Column("is_sourced")]
    public int IsSourced { get; set; }

    [Column("sourced_by", TypeName = "varchar(255)")]
    public string SourcedBy { get; set; } = null!;

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdIssueCredits")]
    public virtual GcdCreatorNameDetail Creator { get; set; } = null!;

    [ForeignKey("CreditTypeId")]
    [InverseProperty("GcdIssueCredits")]
    public virtual GcdCreditType CreditType { get; set; } = null!;

    [ForeignKey("IssueId")]
    [InverseProperty("GcdIssueCredits")]
    public virtual GcdIssue Issue { get; set; } = null!;
}
