using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_story_credit")]
[Index("CreatorId", Name = "idx_gcd_story_credit_gcd_story_credit_creator_id_7c632a78_fk_gcd_creat")]
[Index("CreditTypeId", Name = "idx_gcd_story_credit_gcd_story_credit_credit_type_id_2b1eda8a_fk_gcd_credit_type_id")]
[Index("Deleted", Name = "idx_gcd_story_credit_gcd_story_credit_deleted_5f31a0d2")]
[Index("IsCredited", Name = "idx_gcd_story_credit_gcd_story_credit_is_credited_a0f3090d")]
[Index("IsSigned", Name = "idx_gcd_story_credit_gcd_story_credit_is_signed_da26e307")]
[Index("IsSourced", Name = "idx_gcd_story_credit_gcd_story_credit_is_sourced_b8200d3b")]
[Index("Modified", Name = "idx_gcd_story_credit_gcd_story_credit_modified_1b831920")]
[Index("SignatureId", Name = "idx_gcd_story_credit_gcd_story_credit_signature_id_0c4a25cd_fk_gcd_creat")]
[Index("StoryId", Name = "idx_gcd_story_credit_gcd_story_credit_story_id_7b35c2b5_fk_gcd_story_id")]
[Index("Uncertain", Name = "idx_gcd_story_credit_gcd_story_credit_uncertain_1a69a71c")]
public partial class GcdStoryCredit
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

    [Column("is_signed")]
    public int IsSigned { get; set; }

    [Column("uncertain")]
    public int Uncertain { get; set; }

    [Column("signed_as", TypeName = "varchar(255)")]
    public string SignedAs { get; set; } = null!;

    [Column("credited_as", TypeName = "varchar(255)")]
    public string CreditedAs { get; set; } = null!;

    [Column("credit_name", TypeName = "varchar(255)")]
    public string CreditName { get; set; } = null!;

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [Column("credit_type_id")]
    public int CreditTypeId { get; set; }

    [Column("story_id")]
    public int StoryId { get; set; }

    [Column("signature_id")]
    public int? SignatureId { get; set; }

    [Column("is_sourced")]
    public int IsSourced { get; set; }

    [Column("sourced_by", TypeName = "varchar(255)")]
    public string SourcedBy { get; set; } = null!;

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdStoryCredits")]
    public virtual GcdCreatorNameDetail Creator { get; set; } = null!;

    [ForeignKey("CreditTypeId")]
    [InverseProperty("GcdStoryCredits")]
    public virtual GcdCreditType CreditType { get; set; } = null!;

    [ForeignKey("SignatureId")]
    [InverseProperty("GcdStoryCredits")]
    public virtual GcdCreatorSignature? Signature { get; set; }

    [ForeignKey("StoryId")]
    [InverseProperty("GcdStoryCredits")]
    public virtual GcdStory Story { get; set; } = null!;
}
