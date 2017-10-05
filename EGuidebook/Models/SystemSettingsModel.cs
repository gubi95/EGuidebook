using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EGuidebook.Models
{
    [Table("SystemSettings")]
    public class SystemSettingsModel
    {
        [Key]
        public Guid SystemSettingsId { get; set; }
        public string SMTP_Email { get; set; }
        public string SMTP_Password { get; set; }
        public string SMTP_Host { get; set; }        
        public int SMTP_Port { get; set; }
        public bool SMTP_EnableSSL { get; set; }
    }
}