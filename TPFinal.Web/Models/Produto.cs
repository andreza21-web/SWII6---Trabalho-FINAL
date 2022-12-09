using System.ComponentModel.DataAnnotations;

namespace TPFinal.Web.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]
        public double Preco { get; set; }
        [Display(Name = "Ativo")]
        public bool Status { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public int IdUsuarioUpdate { get; set; }
    }
}
