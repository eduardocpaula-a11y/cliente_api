using ClienteApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConexaoController : ControllerBase
{
    private readonly ConexaoBanco _conexaoBanco;

    public ConexaoController(ConexaoBanco conexaoBanco)
    {
        _conexaoBanco = conexaoBanco;
    }

    [HttpGet("testar")]
    public async Task<IActionResult> TestarConexao()
    {
        try
        {
            await using var conn = _conexaoBanco.ObterConexao();
            await conn.OpenAsync();

            await using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT 1";
            var resultado = await cmd.ExecuteScalarAsync();

            return Ok(new
            {
                sucesso = true,
                mensagem = "Conexão com PostgreSQL realizada com sucesso!",
                resultado
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                sucesso = false,
                mensagem = "Erro ao conectar com o banco de dados.",
                erro = ex.Message
            });
        }
    }
}