using Advogados.Domain.Entities;
using Advogados.Domain.Interfaces;
using Advogados.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Advogados.Infra.Repositories
{
    public class AdvogadoRepository : IAdvogadoRepository
    {
        private readonly AppDbContext _context;

        public AdvogadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Advogado> GetAll()
        {
            return _context.Advogados.ToList();
        }

        public Advogado GetById(int id)
        {
            return _context.Advogados.Find(id);
        }

        public void Add(Advogado advogado)
        {
            _context.Advogados.Add(advogado);
            _context.SaveChanges();  // Salva as mudanças após adicionar
        }

        public void Update(Advogado advogado)
        {
            _context.Entry(advogado).State = EntityState.Modified;
            _context.SaveChanges();  // Salva as mudanças após atualizar
        }

        public void Delete(Advogado advogado)
        {
            _context.Advogados.Remove(advogado);
            _context.SaveChanges();  // Salva as mudanças após deletar
        }

        public bool Exists(int id)
        {
            return _context.Advogados.Any(a => a.Id == id);
        }

        public void AlterarStatus(int id, bool ativo)
        {
            var advogado = GetById(id);
            if (advogado != null)
            {
                advogado.Ativo = ativo;
                Update(advogado);  
            }
        }
    }
}
