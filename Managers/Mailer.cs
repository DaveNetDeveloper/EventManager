using EventManager.Common;
using EventManager.Models.System;
using System;
using System.Net;
using System.Net.Mail; 

namespace EventManager.Managers
{
    public class Mailer
    {
        public static bool SendMail(MailSettings datosContacto)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(Coder.Base64Decode(Settings.MailFrom), Coder.Base64Decode(Settings.MailTo))
                {
                    IsBodyHtml = true,
                    //mailMessage.Subject = Resources.resource.Mail_Subject; 
                    Body = GetUpdatedMailContent(datosContacto)
                };

                SmtpClient smtpClient = new SmtpClient(Coder.Base64Decode(Settings.MailHost), int.Parse(Coder.Base64Decode(Settings.MailPort)));
                //smtpClient.EnableSsl = bool.Parse(Settings.MailEnableSsl);

                var credentials = new NetworkCredential(Coder.Base64Decode(Settings.MailUser), Coder.Base64Decode(Settings.MailPassword));

                smtpClient.Credentials = credentials;
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string GetUpdatedMailContent(MailSettings emailContact)
        {
            try
            {
                //string mailBody = Resources.resource.Mail_Body;

                string mailBody = "<br/> <b>Nombre completo: </b>" + emailContact.Email_Content.FullName + "<br />";
                mailBody += "<b>Correo electrónico: </b>" + emailContact.Email_Content.Email + "<br />";
                mailBody += "<b>Teléfono: </b>" + emailContact.Email_Content.Phone + "<br />";
                mailBody += "<b>Asunto del mensaje: </b>" + emailContact.Email_Content.Subject + "<br />";
                mailBody += "<b>Mensaje: </b>" + emailContact.Email_Content.Message + "<br />";
                mailBody += "<br />";
                mailBody += "Atentamente, " + "<br />";
                //mailBody += "Corporation</div>";  
                mailBody += "<table style = 'float: right; width: 100%' id = 'tLogo'> <tr><td style ='float:right;'><a target='_blank' href='http://www.com'><img src='http://www' width='135px' height='85px' /></a></td></tr></table>";
                return mailBody;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}