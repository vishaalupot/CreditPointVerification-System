using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;
using System.IO;

namespace CPV_Mark3.Models
{
    public class UserRolesViewModel
    {
        [Key]
        public int snr { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
    public class RegisterViewModel
    {
        //[Required]
        //[StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        //[Required]
        //[StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "UserRole")]
        public string UserRole { get; set; }

        //[Required]
        //[StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Password")]
        public string Password { get;set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Employee Code")]
        //public string EmpCode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Roles")]
        public List<string> ListRoles { get; set; }

        //[Required]
        [DataType(DataType.Text)]
        [Display(Name = "Role")]
        public string InitialRole { get; set; }


    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string UserName { get; set; }


    }

    //public class z
    //{
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "FullName")]
    //    public string FullName { get; set; }

    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Status")]
    //    public string Status { get; set; }

    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "UserRole")]
    //    public string UserRole { get; set; }

    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "UserName")]
    //    public string UserName { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }



    //}

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ManageRoles
    {
        [Key]
        [Required]
        public string ID { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string Name { get; set; }
    }

    public partial class Image
    {
        internal static Image FromStream(MemoryStream ms)
        {
            throw new NotImplementedException();
        }

        internal void Save(MemoryStream imageStream, ImageFormat png)
        {
            throw new NotImplementedException();
        }
    }
}

