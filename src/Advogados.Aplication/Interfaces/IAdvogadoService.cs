using Advogados.Application.DTOs;
using System.Collections.Generic;

namespace Advogados.Application.Interfaces
{
    public interface IAdvogadoService
    {
        IEnumerable<AdvogadoDto> GetAll();
        AdvogadoDto GetById(int id);
        void CadastrarAdvogado(AdvogadoDto advogadoDto);
        void AtualizarAdvogado(int id, AdvogadoDto advogadoDto);
        void DeletarAdvogado(int id);
        bool Exists(int id);
        string AlterarStatusAdvogado(int id); 
    }
}
