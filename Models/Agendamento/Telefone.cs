using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento_de_Consulta.Models.Agendamento
{
    [Table("TELEFONE")]
    public class TELEFONE
    {
        public TELEFONE()
        {
            this.Teltip = new TELEFONETIPO();
        }

        public int IdTel { get; set; }
        [Required]
        public int NUMERO { get; set; }
        [Required]
        public int DDD { get; set; }
        [Required]
        public TELEFONETIPO Teltip { get; private set; }

    }
}
