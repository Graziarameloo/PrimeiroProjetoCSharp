using System.Collections.Generic;

namespace Agendamento_de_Consulta.Models.Agendamento
{
    public interface IPessoasDAL
    {
        IEnumerable<PESSOAS> GetAllPESSOAS();
        int AddPessoas(PESSOAS pessoas);
        int AddEndereco(PESSOAS pessoas);
        void AddPessoaTelefone(int idPessoas, int idTelefone);
        int AddTelefone(PESSOAS pessoas);


        int UpdatePessoas(PESSOAS pessoas);
        int UpdateEndereco(PESSOAS pessoas);
        int UpdatePessoaTelefone(int idPessoas, int idTelefone);
        int UpdateTelefone(PESSOAS pessoas);


        PESSOAS GetPessoas(int? id);
        void DeletePessoaTelefone(int idPessoas, int idTelefone);
        void DeletePessoas(int? id);
        void DeleteEndereco(int? id);
        void DeleteTelefone(int? id);

    }
}
