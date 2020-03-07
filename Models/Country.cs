using System.ComponentModel.DataAnnotations;

namespace EventManager
{

    public class Country : IDbEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}