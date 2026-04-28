using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Table("gcd_creator_signature")]
[Index("CreatorId", Name = "idx_gcd_creator_signature_gcd_creator_signature_creator_id_063b6789_fk_gcd_creator_id")]
[Index("Deleted", Name = "idx_gcd_creator_signature_gcd_creator_signature_deleted_2e7249b9")]
[Index("Modified", Name = "idx_gcd_creator_signature_gcd_creator_signature_modified_064658ce")]
[Index("Name", Name = "idx_gcd_creator_signature_gcd_creator_signature_name_91dfd0cc")]
public partial class GcdCreatorSignature
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

    [Column("notes", TypeName = "longtext")]
    public string Notes { get; set; } = null!;

    [Column("generic")]
    public int Generic { get; set; }

    [Column("creator_id")]
    public int CreatorId { get; set; }

    [ForeignKey("CreatorId")]
    [InverseProperty("GcdCreatorSignatures")]
    public virtual GcdCreator Creator { get; set; } = null!;

    [InverseProperty("Signature")]
    public virtual ICollection<GcdStoryCredit> GcdStoryCredits { get; set; } = new List<GcdStoryCredit>();
}
