using System.ComponentModel.DataAnnotations;

namespace EventManager
{ 
    public class Language : IEntity 
    {
        [Required]
        public int Id { get; set; }
        [Required] [StringLength(250)]
        public string Name { get; set; }
        [Required] [StringLength(5)]
        public string Code { get; set; }
    }
}