using CadastroEmpresasApi.DTOs;
using System.Text.Json;

namespace CadastroEmpresasApi.Services;

public class ReceitaWsService
{
    private readonly HttpClient _httpClient;

    public ReceitaWsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EmpresaDto?> BuscarEmpresaPorCnpj(string cnpj)
    {
        try
        {
            var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            var empresa = JsonSerializer.Deserialize<EmpresaDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return empresa;
        }
        catch
        {
            return null;
        }
    }
}
