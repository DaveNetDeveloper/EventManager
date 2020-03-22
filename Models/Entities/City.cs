using System.ComponentModel.DataAnnotations;

namespace EventManager
{ 
    public class City : IDbEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}