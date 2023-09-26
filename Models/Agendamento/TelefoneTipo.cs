using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento_de_Consulta.Models.Agendamento
{
    [Table("TELEFONE_TIPO")]
    public class TELEFONETIPO
    {
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
    }
}
