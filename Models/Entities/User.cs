using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager
{ 
    public class User : IDbEntity
    {
        [Required]
        public int Id { get; set; }
        [Required] [StringLength(250)]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; } 
    }
}