using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager
{
    public class Company : IEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int Phone { get; set; }
        [StringLength(100)]
        public string Email { get; set; }

        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
    }
}