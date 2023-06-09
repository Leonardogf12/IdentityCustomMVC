﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Forms;

namespace IdentityCustomMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string? ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(255)]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(50)]
        [DisplayName("CPF")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "Data no formato incorreto.")]        
        [DisplayName("Data de Nascimento")]
        public DateTime BirthDate { get; set; }
        
        [MaxLength(10)]
        [DisplayName("Cep")]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(255)]
        [DisplayName("Endereço")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(20)]
        [DisplayName("Contato")]
        public string? CellPhone { get; set; }
   
        [DisplayName("Status")]
        public bool? Status { get; set; }
    }
}
