using AutoMapper;
using Advogados.Application.DTOs;
using Advogados.Application.Interfaces;
using Advogados.Domain.Entities;
using Advogados.Domain.Interfaces;
using Advogados.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Advogados.Domain.Enum;

namespace Advogados.Application.Services
{
    public class AdvogadoService : IAdvogadoService
    {
        private readonly IAdvogadoRepository _advogadoRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public AdvogadoService(IAdvogadoRepository advogadoRepository, IMapper mapper, AppDbContext context)
        {
            _advogadoRepository = advogadoRepository;
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<AdvogadoDto> GetAll()
        {
            try
            {
                var advogados = _context.Advogados
                    .Include(a => a.Enderecos)
                    .ToList();

                return _mapper.Map<IEnumerable<AdvogadoDto>>(advogados);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar advogados.", ex);
            }
        }

        public AdvogadoDto GetById(int id)
        {
            try
            {
                var advogado = _context.Advogados
                    .Include(a => a.Enderecos)
                    .FirstOrDefault(a => a.Id == id);

                if (advogado == null)
                {
                    throw new Exception("Advogado não encontrado.");
                }

                return _mapper.Map<AdvogadoDto>(advogado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar advogado pelo ID: {id}.", ex);
            }
        }

        public void CadastrarAdvogado(AdvogadoDto advogadoDto)
        {
            try
            {
                // Convertendo a string para o enum TipoDeAdvogado
                var tipoAdvogado = (TipoDeAdvogado)Enum.Parse(typeof(TipoDeAdvogado), advogadoDto.Tipo, true);

                // Criando a entidade Advogado a partir do DTO
                var advogado = _mapper.Map<Advogado>(advogadoDto);
                advogado.Tipo = tipoAdvogado; // Atribuindo o enum convertido à propriedade Tipo

                foreach (var endereco in advogado.Enderecos)
                {
                    endereco.Estado = (Estado)Enum.Parse(typeof(Estado), endereco.Estado.ToString(), true); // Convertendo o estado para enum
                }

                _advogadoRepository.Add(advogado);
                advogadoDto.Id = advogado.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar advogado.", ex);
            }
         }

        public void AtualizarAdvogado(int id, AdvogadoDto advogadoDto)
        {
            try
            {
                var advogado = _context.Advogados
                    .Include(a => a.Enderecos)
                    .FirstOrDefault(a => a.Id == id);

                if (advogado == null)
                {
                    throw new Exception("Advogado não encontrado.");
                }

                // Convertendo a string para o enum TipoDeAdvogado
                var tipoAdvogado = (TipoDeAdvogado)Enum.Parse(typeof(TipoDeAdvogado), advogadoDto.Tipo, true);

                advogado.Nome = advogadoDto.Nome;
                advogado.Tipo = tipoAdvogado; // Atribuindo o enum convertido à propriedade Tipo
                advogado.Ativo = advogadoDto.Ativo;

                // Atualizando endereços
                foreach (var enderecoDto in advogadoDto.Enderecos)
                {
                    var enderecoExistente = advogado.Enderecos
                        .FirstOrDefault(e => e.Id == enderecoDto.Id);

                    var estado = (Estado)Enum.Parse(typeof(Estado), enderecoDto.Estado, true); // Convertendo o estado para enum

                    if (enderecoExistente != null)
                    {
                        enderecoExistente.Logradouro = enderecoDto.Logradouro;
                        enderecoExistente.Bairro = enderecoDto.Bairro;
                        enderecoExistente.Estado = estado;
                        enderecoExistente.CEP = enderecoDto.CEP;
                        enderecoExistente.Numero = enderecoDto.Numero;
                        enderecoExistente.Complemento = enderecoDto.Complemento;
                    }
                    else
                    {
                        var novoEndereco = _mapper.Map<Endereco>(enderecoDto);
                        novoEndereco.Estado = estado;
                        advogado.Enderecos.Add(novoEndereco);
                    }
                }

                var enderecosParaRemover = advogado.Enderecos
                    .Where(e => !advogadoDto.Enderecos.Any(dto => dto.Id == e.Id))
                    .ToList();

                foreach (var enderecoRemover in enderecosParaRemover)
                {
                    _context.Enderecos.Remove(enderecoRemover);
                }

                _context.Advogados.Update(advogado);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar advogado com ID: {id}.", ex);
            }
        }

        public void DeletarAdvogado(int id)
        {
            try
            {
                var advogado = _advogadoRepository.GetById(id);
                if (advogado == null)
                {
                    throw new Exception("Advogado não encontrado.");
                }

                _advogadoRepository.Delete(advogado);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar advogado com ID: {id}.", ex);
            }
        }

        public bool Exists(int id)
        {
            try
            {
                return _advogadoRepository.Exists(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar existência do advogado.", ex);
            }
        }

        public string AlterarStatusAdvogado(int id)
        {
            var advogado = _advogadoRepository.GetById(id);
            if (advogado != null)
            {
                // Altera o status de ativo para inativo, ou de inativo para ativo
                advogado.Ativo = !advogado.Ativo;

                _advogadoRepository.Update(advogado);
                _context.SaveChanges(); // Chama SaveChanges no contexto, não no repositório

                // Retorna uma mensagem com base no novo status
                return advogado.Ativo ? "Status ativado com sucesso." : "Status inativado com sucesso.";
            }

            return "Advogado não encontrado.";
        }
    }
}
