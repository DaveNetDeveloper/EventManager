using System.Web.Configuration;

namespace EventManager.Common
{
    internal static class Settings
    {
        #region [ Proyect Configurations ]

        public static string ProyectName { get; } = WebConfigurationManager.AppSettings["ProyectName"]; 

        #endregion

        #region [ Language Configurations ]

        public static string DefaultLanguage { get; set; } = WebConfigurationManager.AppSettings["settings_DefaultLanguage"];

        #endregion

        #region [ Log Configurations ]

        public static string LogXmlVisitorFileName { get; } = WebConfigurationManager.AppSettings["settings_XmlVisitorLogFileName"];
        public static string LogXmlContactFileName { get; } = WebConfigurationManager.AppSettings["settings_XmlContactLogFileName"];
        public static string LogTextFileName { get; } = WebConfigurationManager.AppSettings["settings_TextLogFileName"];

        #endregion

        #region [ Directory Configurations ]

        public static string DataDirectory { get; } = WebConfigurationManager.AppSettings["settings_DataDirectory"];
        public static string FilePathDelimiter { get; } = @"\";
        public static string RootStartPath { get; } = @"~/";
        public static string ParamNameQueryStringSubject { get; } = @"S";

        #endregion

        #region [ Mail Configurations ]

        public static string MailFrom { get; } = WebConfigurationManager.AppSettings["Mail_From"];
        public static string MailTo { get; } = WebConfigurationManager.AppSettings["Mail_To"];
        public static string MailHost { get; } = WebConfigurationManager.AppSettings["Mail_Host"];
        public static string MailPort { get; } = WebConfigurationManager.AppSettings["Mail_Port"];
        public static string MailUser { get; } = WebConfigurationManager.AppSettings["Mail_User"];
        public static string MailPassword { get; } = WebConfigurationManager.AppSettings["Mail_Password"];
        public static string MailEnableSsl { get; } = WebConfigurationManager.AppSettings["Mail_EnableSsl"];

        #endregion
    }
}