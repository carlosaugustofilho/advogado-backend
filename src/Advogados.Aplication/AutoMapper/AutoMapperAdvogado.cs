using AutoMapper;
using Advogados.Application.DTOs;
using Advogados.Domain.Entities;
using Advogados.Domain.Enum;

namespace Advogados.Application.AutoMapper
{
    public class AutoMapperAdvogado : Profile
    {
        public AutoMapperAdvogado()
        {
            // Mapeamento de AdvogadoDto para Advogado
            CreateMap<AdvogadoDto, Advogado>()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
             .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
             .ForMember(dest => dest.Ativo, opt => opt.MapFrom((AdvogadoDto src) => src.Ativo))  // Tipo especificado
             .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos));

            // Mapeamento de EnderecoDto para Endereco
            CreateMap<EnderecoDto, Endereco>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src =>  src.Estado));

            // Mapeamento de Advogado para AdvogadoDto
            CreateMap<Advogado, AdvogadoDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dest => dest.Enderecos, opt => opt.MapFrom(src => src.Enderecos));

            // Mapeamento de Endereco para EnderecoDto
            CreateMap<Endereco, EnderecoDto>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => (Estado)src.Estado));
        }
    }
}
