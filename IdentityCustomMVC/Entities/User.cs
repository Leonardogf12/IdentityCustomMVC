using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace IdentityCustomMVC.Entities
{
    [NotMapped]
    public class User
    {
        [Column("USR_NAME")]
        [MaxLength(255)]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Column("USR_CPF")]
        [MaxLength(50)]
        [DisplayName("CPF")]
        public string? Cpf { get; set; }

        [Column("USR_BIRTH_DATE")]
        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Column("USR_CEP")]
        [MaxLength(255)]
        [DisplayName("Cep")]
        public string? Cep { get; set; }

        [Column("USR_ADDRESS")]
        [MaxLength(255)]
        [DisplayName("Endereço")]
        public string? Address { get; set; }

        [Column("USR_CELL_PHONE")]
        [MaxLength(20)]
        [DisplayName("Celular")]
        public string? CellPhone { get; set; }

        [Column("USR_STATUS")]
        [DisplayName("Status")]
        public bool? Status { get; set; }
    }
}
