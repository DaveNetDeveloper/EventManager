using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager//EventManager.Models
{
    public class Event : IDbEntity, IAuditEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Capacity { get; set; }
        public DateTime? EventDateTime { get; set; }
        public int? Sessions { get; set; }
        public bool? Active { get; set; }        
        public int CompanyId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; }
        [ForeignKey("CompanyId")]
        public Company Company { get; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}