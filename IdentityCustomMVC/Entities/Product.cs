using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityCustomMVC.Entities
{
    public class Product
    {
        [Key]
        [Column("PRO_ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DisplayName("Nome")]
        [Column("PRO_NAME")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DisplayName("Descrição")]
        [Column("PRO_DESCRIPTION")]
        public string? Description { get; set; }
    }
}
