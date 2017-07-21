using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

namespace EGuidebook.Models
{
    public class LoginViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Prosze podać adres e-mail")]
        [EmailAddress(ErrorMessage = "Podany adres e-mail jest nieprawidłowy")]
        public string LoginEmail { get; set; }
                
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Prosze podać hasło")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać conajmniej {2} znaków", MinimumLength = 6)]
        [Display(Name = "Hasło")]
        public string LoginPassword { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Prosze podać adres e-mail")]
        [EmailAddress(ErrorMessage = "Podany adres e-mail jest nieprawidłowy")]        
        public string RegisterEmail { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Prosze podać hasło")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać conajmniej {2} znaków", MinimumLength = 6)]
        [Display(Name = "Hasło")]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Hasło musi zawierać conajmniej 1 z symboli: !, @, # itp.",
            ErrorMessage = "Hasło musi zawierać conajmniej 6 znaków i 1 z symboli: !, @, # itp."
        )]
        public string RegisterPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Prosze podać hasło")]
        [Compare("RegisterPassword", ErrorMessage = "Hasła nie są identyczne")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać conajmniej {2} znaków", MinimumLength = 6)]
        [Display(Name = "Powtórz hasło")]
        public string RegisterPasswordRepeat { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Prosze podać adres e-mail")]
        [EmailAddress(ErrorMessage = "Podany adres e-mail jest nieprawidłowy")]
        public string PasswordResetEmail { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Prosze podać adres e-mail")]
        [EmailAddress(ErrorMessage = "Podany adres e-mail jest nieprawidłowy")]
        public string Username { get; set; }
        
        [Display(Name = "Tymczasowe hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Proszę podać tymczasowe hasło")]
        public string TempPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Prosze podać hasło")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać conajmniej {2} znaków", MinimumLength = 6)]
        [Display(Name = "Hasło")]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Hasło musi zawierać conajmniej 1 z symboli: !, @, # itp.",
            ErrorMessage = "Hasło musi zawierać conajmniej 6 znaków i 1 z symboli: !, @, # itp."
        )]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Prosze podać hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są identyczne")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać conajmniej {2} znaków", MinimumLength = 6)]
        [Display(Name = "Powtórz hasło")]
        public string NewPasswordRepeat { get; set; }
    }
}
