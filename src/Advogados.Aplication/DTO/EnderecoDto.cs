using Advogados.Domain.Enum;

namespace Advogados.Application.DTOs
{
    public class EnderecoDto
    {
        public int Id { get; set; }  // Adiciona a propriedade Id
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }

}
