using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using EGuidebook.Models;
using System.Text;

namespace EGuidebook.Shared
{
    public class SMTPManager
    {
        public static void SendResetPasswordEmail(string strNewPassword, string strSendToEmail)
        {
            StringBuilder objStringBuilder = new StringBuilder();
            objStringBuilder.AppendLine("<div style='color: #3F3F3F;'><h2>EGuidebook - resetowanie hasła</h2><br/>");
            objStringBuilder.AppendFormat("Witaj! W celu zresetowania hasła proszę {0}zalogować się{1} do portalu używająć poniższego hasła:", 
                "<a href='" + Helper.BaseURL + "/Account/ChangePassword" + "'>", "</a>");
            objStringBuilder.AppendFormat("<br/><br/><b>{0}</b><br/><br/>", strNewPassword);
            objStringBuilder.AppendLine("Po zalogowaniu nastąpi przekierowanie do właściwej zmiany hasła.<br/><br/>");
            objStringBuilder.AppendLine("Pozdrawiamy i życzymy miłego dnia - zespół EGuidebook.</div>");

            SendEmail("EGuidebook - resetowanie hasła", objStringBuilder.ToString(), strSendToEmail, true);
        }

        public static void SendEmail(string strSubject, string strMessage, string strSendToEmail, bool bIsBodyHTML = false)
        {
            try
            {
                ApplicationDbContext objApplicationDbContext = new ApplicationDbContext();
                List<SystemSettingsModel> listSystemSettingsModel = objApplicationDbContext.SystemSettings.ToList();
                SystemSettingsModel objSystemSettingsModel = listSystemSettingsModel.Count > 0 ? listSystemSettingsModel[0] : null;

                if (objSystemSettingsModel != null)
                {
                    string strEmail = objSystemSettingsModel.SMTP_Email;
                    string strPassword = objSystemSettingsModel.SMTP_Password;
                    int nPort = objSystemSettingsModel.SMTP_Port;
                    string strHost = objSystemSettingsModel.SMTP_Host;
                    bool bEnableSSL = objSystemSettingsModel.SMTP_EnableSSL;

                    SmtpClient objSmtpClient = new SmtpClient();
                    objSmtpClient.Port = nPort;
                    objSmtpClient.Host = strHost;
                    objSmtpClient.EnableSsl = bEnableSSL;
                    objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    objSmtpClient.UseDefaultCredentials = false;
                    objSmtpClient.Credentials = new System.Net.NetworkCredential(strEmail, strPassword);

                    MailMessage objMailMessage = new MailMessage(strEmail, strSendToEmail, strSubject, strMessage);
                    objMailMessage.BodyEncoding = UTF8Encoding.UTF8;
                    objMailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    objMailMessage.IsBodyHtml = bIsBodyHTML;

                    objSmtpClient.Send(objMailMessage);
                }
            }
            catch(Exception ex) { }
        }
    }
}