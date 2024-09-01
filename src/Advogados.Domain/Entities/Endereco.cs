using Advogados.Domain.Entities;
using Advogados.Domain.Enum;

public class Endereco
{
    public int Id { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public Estado Estado { get; set; }  // Setter público para Estado
    public string CEP { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public int AdvogadoId { get; set; }
    public virtual Advogado Advogado { get; set; }
}
