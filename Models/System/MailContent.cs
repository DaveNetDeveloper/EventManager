using System;

namespace EventManager.Models.System
{
    public class MailContent
    {
        public MailContent()
        {
        } 
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}