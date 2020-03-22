using System.ComponentModel.DataAnnotations;

namespace EventManager
{ 
    public class Login
    {
        [Required]
        public string Username { get; set; }
        [Required] 
        public string Password { get; set; }
        public bool RemeberAccount { get; set; }
    }
}