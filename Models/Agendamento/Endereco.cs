using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Agendamento_de_Consulta.Models.Agendamento
{
    [Table("ENDERECO")]

    public class Endereco
    {
        [Key]
        public int IdEndereco { get; set; }
        [Required]
        public string LOGRADOURO { get; set; }

        public int NUMEROCASA { get; set; }

        public int CEP { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string ESTADO { get; set; }

    }


}


