using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    public class SystemSettingsViewModel
    {
        public Guid SystemSettingsId { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DisplayName("Adres e-mail")]
        [EmailAddress(ErrorMessage = "Prosze wprowadzić poprawny adres e-mail")]
        public string SMTP_Email { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DisplayName("Hasło")]
        public string SMTP_Password { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DisplayName("Host")]
        public string SMTP_Host { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DisplayName("Port")]
        public int SMTP_Port { get; set; }
        [DisplayName("Włącz SSL")]
        public bool SMTP_EnableSSL { get; set; }

        public SystemSettingsViewModel() { }
        public SystemSettingsViewModel(SystemSettingsModel objSystemSettingsModel)
        {
            this.SystemSettingsId = objSystemSettingsModel.SystemSettingsId;
            this.SMTP_Email = objSystemSettingsModel.SMTP_Email;
            this.SMTP_Password = objSystemSettingsModel.SMTP_Password;
            this.SMTP_Host = objSystemSettingsModel.SMTP_Host;
            this.SMTP_Port = objSystemSettingsModel.SMTP_Port;
            this.SMTP_EnableSSL = objSystemSettingsModel.SMTP_EnableSSL;
        }

        public void ApplyOnModel(ref SystemSettingsModel objSystemSettingsModel)
        {
            objSystemSettingsModel.SMTP_Email = this.SMTP_Email;
            objSystemSettingsModel.SMTP_Password = this.SMTP_Password;
            objSystemSettingsModel.SMTP_Host = this.SMTP_Host;
            objSystemSettingsModel.SMTP_Port = this.SMTP_Port;
            objSystemSettingsModel.SMTP_EnableSSL = this.SMTP_EnableSSL;
        }
    }
}