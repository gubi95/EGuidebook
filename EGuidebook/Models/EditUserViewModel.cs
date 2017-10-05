using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EGuidebook.Models
{
    public class EditUserViewModel
    {           
        [Required(ErrorMessage = "Proszę podać nazwę użytkownika")]
        [Display(Name = "Nazwa użytkownika")]
        [EmailAddress(ErrorMessage = "Proszę podać poprawny adres e-mail")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Proszę wybrać role")]
        [Display(Name = "Rola")]
        public string RoleId { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} musi zawierać conajmniej {2} znaków", MinimumLength = 6)]
        [Display(Name = "Hasło")]
        [MembershipPassword(
            MinRequiredNonAlphanumericCharacters = 1,
            MinNonAlphanumericCharactersError = "Hasło musi zawierać conajmniej 1 z symboli: !, @, # itp.",
            ErrorMessage = "Hasło musi zawierać conajmniej 6 znaków i 1 z symboli: !, @, # itp."
        )]
        public string UserPassword { get; set; }

        [DataType(DataType.Password)]        
        [Compare("UserPassword", ErrorMessage = "Hasła nie są identyczne")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać conajmniej {2} znaków", MinimumLength = 6)]
        [Display(Name = "Powtórz hasło")]
        public string UserPasswordRepeat { get; set; }

        [Display(Name = "Imie")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Zdjęcie")]
        public string AvatarImagePath { get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public EditUserViewModel() { }
        public EditUserViewModel(List<RoleViewModel> listRoleViewModel)
        {
            this.Roles = listRoleViewModel;
        }

        public EditUserViewModel(List<RoleViewModel> listRoleViewModel, ApplicationUser objApplicationUser)
        {
            this.Username = objApplicationUser.UserName;
            this.RoleId = objApplicationUser.Roles.ToList()[0].RoleId;
            this.UserPassword = "";
            this.UserPasswordRepeat = "";
            this.FirstName = objApplicationUser.FirstName;
            this.LastName = objApplicationUser.LastName;
            this.AvatarImagePath = objApplicationUser.AvatarImagePath;

            this.Roles = listRoleViewModel;
        }
    }
}