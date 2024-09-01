using Advogados.Domain.Enum;
using System.Collections.Generic;

namespace Advogados.Application.DTOs
{
    public class AdvogadoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public bool Ativo { get; set; }
        public List<EnderecoDto> Enderecos { get; set; }
    }
}

