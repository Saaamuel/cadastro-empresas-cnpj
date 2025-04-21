using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using CadastroEmpresas.API.Data;
using CadastroEmpresas.API.Models;
using CadastroEmpresas.API.Dtos;

namespace CadastroEmpresas.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IHttpClientFactory _httpFactory;

    public CompaniesController(DataContext context, IHttpClientFactory httpFactory)
    {
        _context = context;
        _httpFactory = httpFactory;
    }

    [HttpPost]
    public async Task<ActionResult> AddCompany([FromBody] string cnpj)
    {
        var client = _httpFactory.CreateClient();
        var response = await client.GetAsync($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");

        if (!response.IsSuccessStatusCode)
            return BadRequest("Erro ao consultar CNPJ.");

        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<ReceitaWsDto>(content);

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var company = new Company
        {
            Cnpj = data.Cnpj,
            NomeEmpresarial = data.Nome,
            NomeFantasia = data.Fantasia,
            Situacao = data.Situacao,
            Abertura = data.Abertura,
            Tipo = data.Tipo,
            NaturezaJuridica = data.NaturezaJuridica,
            AtividadePrincipal = data.Atividade_principal.FirstOrDefault()?.Text,
            Logradouro = data.Logradouro,
            Numero = data.Numero,
            Complemento = data.Complemento,
            Bairro = data.Bairro,
            Municipio = data.Municipio,
            Uf = data.Uf,
            Cep = data.Cep,
            UserId = userId
        };

        _context.Companies.Add(company);
        await _context.SaveChangesAsync();

        return Ok("Empresa cadastrada com sucesso.");
    }

    [HttpGet]
    public async Task<ActionResult<List<Company>>> GetCompanies()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var companies = await _context.Companies
            .Where(c => c.UserId == userId)
            .ToListAsync();

        return Ok(companies);
    }
}
