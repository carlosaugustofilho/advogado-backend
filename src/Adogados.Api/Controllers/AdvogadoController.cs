using Advogados.Application.DTOs;
using Advogados.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Advogados.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvogadoController : ControllerBase
    {
        private readonly IAdvogadoService _advogadoService;

        public AdvogadoController(IAdvogadoService advogadoService)
        {
            _advogadoService = advogadoService;
        }

        [HttpGet("ListarAdvogados")]
        public IActionResult GetAll()
        {
            try
            {
                var advogados = _advogadoService.GetAll();
                return Ok(advogados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("BuscarPorId/{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                var advogado = _advogadoService.GetById(id);
                if (advogado == null)
                {
                    return NotFound("Advogado não encontrado.");
                }
                return Ok(advogado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("CadastrarAdvogado")]
        public IActionResult CadastrarAdvogado([FromBody] AdvogadoDto advogadoDto)
        {
            try
            {
                _advogadoService.CadastrarAdvogado(advogadoDto);
                return CreatedAtAction(nameof(BuscarPorId), new { id = advogadoDto.Id }, advogadoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cadastrar advogado: {ex.Message}");
            }
        }

        [HttpPut("AtualizarAdvogado/{id}")]
        public IActionResult AtualizarAdvogado(int id, [FromBody] AdvogadoDto advogadoDto)
        {
            try
            {
                if (!_advogadoService.Exists(id))
                {
                    return NotFound("Advogado não encontrado.");
                }

                _advogadoService.AtualizarAdvogado(id, advogadoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar advogado: {ex.Message}");
            }
        }

        [HttpDelete("DeletarAdvogado/{id}")]
        public IActionResult DeletarAdvogado(int id)
        {
            try
            {
                if (!_advogadoService.Exists(id))
                {
                    return NotFound("Advogado não encontrado.");
                }

                _advogadoService.DeletarAdvogado(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar advogado: {ex.Message}");
            }
        }

        [HttpPut("AlterarStatusAdvogado/{id}")]
        public IActionResult AlterarStatusAdvogado(int id)
        {
            if (!_advogadoService.Exists(id))
            {
                return NotFound("Advogado não encontrado."); // Retorna 404 se o advogado não for encontrado
            }

            var resultado = _advogadoService.AlterarStatusAdvogado(id);
            return Ok(resultado); // Retorna a mensagem de sucesso ou erro
        }

    }
}
