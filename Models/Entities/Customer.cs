using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager
{
    public class Customer : IDbEntity, IAuditEntity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int LanguageId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public IEnumerable<Company> Companies { get; set; }
    }
}