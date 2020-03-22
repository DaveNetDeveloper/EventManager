namespace EventManager.Models.System
{
    public class MailSettings
    {
        public MailSettings()
        {
        }

        public MailContent Email_Content { get; set; }

        public string Email_SMTP_Address { get; set; }
        public string Email_Port { get; set; }
        public string Email_From { get; set; }
        public string Email_To { get; set; }
        public string Email_Account_User { get; set; }
        public string Email_Account_Password { get; set; }
    }
}