using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations;

namespace IdentityCustomMVC.Models
{
    public class ResetPasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
       
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }

        public bool IsSuccess { get; set; }

        //***************************************************
        //*OUTRA TENTATIVA

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
       
        public string Token { get; set; }

    }
}
