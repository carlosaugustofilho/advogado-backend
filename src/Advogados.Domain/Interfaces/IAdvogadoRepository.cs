using Advogados.Domain.Entities;
using System.Collections.Generic;

namespace Advogados.Domain.Interfaces
{
    public interface IAdvogadoRepository
    {
        IEnumerable<Advogado> GetAll();        // Método para obter todos os advogados
        Advogado GetById(int id);              // Método para obter um advogado por ID
        void Add(Advogado advogado);           // Método para adicionar um novo advogado
        void Update(Advogado advogado);        // Método para atualizar um advogado existente
        void Delete(Advogado advogado);        // Método para remover um advogado
        bool Exists(int id);                   // Método para verificar se um advogado existe
        void AlterarStatus(int id, bool ativo); // Método para alterar o status (ativo/inativo) de um advogado
    }
}
