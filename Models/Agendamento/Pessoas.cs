using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento_de_Consulta.Models.Agendamento


{
    [Table("PESSOA")]
    public class PESSOAS
    {
        public PESSOAS()
        {
            this.End = new Endereco();
            this.Tel = new TELEFONE();

        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public long CPF { get; set; }
        public Endereco End { get; set; }
        public TELEFONE Tel { get; set; }

    }
}
