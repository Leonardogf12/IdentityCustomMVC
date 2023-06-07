﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IdentityCustomMVC.Models
{
    public class ForgotPasswordViewModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [DisplayName("Email do Registro.")]
        public string? Email { get; set; }

        public bool EmailSent { get; set; }

    }
}
