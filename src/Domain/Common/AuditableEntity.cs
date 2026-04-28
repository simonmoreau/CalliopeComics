using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class AuditableEntity
    {
        [MaxLength(320)]
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }

        [MaxLength(320)]
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public DateTime? Deleted { get; set; }

        protected internal AuditableEntity()
        {
            Created = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }
    }
}