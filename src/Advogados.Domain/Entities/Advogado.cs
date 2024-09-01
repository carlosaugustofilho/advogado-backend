using Advogados.Domain.Enum;
using System.Collections.Generic;

namespace Advogados.Domain.Entities
{
    public class Advogado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoDeAdvogado Tipo { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Endereco> Enderecos { get; set; }
    }
}
