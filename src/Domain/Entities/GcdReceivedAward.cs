using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_received_award")]
[Index("AwardId", Name = "idx_gcd_received_award_gcd_received_award_award_id_a1daee53_fk_gcd_award_id")]
[Index("ContentTypeId", Name = "idx_gcd_received_award_gcd_received_award_content_type_id_76c10020_fk_django_co")]
[Index("Deleted", Name = "idx_gcd_received_award_gcd_received_award_deleted_db659429")]
[Index("Modified", Name = "idx_gcd_received_award_gcd_received_award_modified_dfc45335")]
public partial class GcdReceivedAward
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

    [Column("object_id")]
    public int? ObjectId { get; set; }

    [Column("award_name", TypeName = "varchar(255)")]
    public string AwardName { get; set; } = null!;

    [Column("no_award_name")]
    public int NoAwardName { get; set; }

    [Column("award_year")]
    public int? AwardYear { get; set; }

    [Column("award_year_uncertain")]
    public int AwardYearUncertain { get; set; }

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("award_id")]
    public int? AwardId { get; set; }

    [Column("content_type_id")]
    public int? ContentTypeId { get; set; }

    [ForeignKey("AwardId")]
    [InverseProperty("GcdReceivedAwards")]
    public virtual GcdAward? Award { get; set; }

    [ForeignKey("ContentTypeId")]
    [InverseProperty("GcdReceivedAwards")]
    public virtual DjangoContentType? ContentType { get; set; }
}
