namespace CadastroEmpresas.API.Models;

public class Company
{
    public int Id { get; set; }
    public string Cnpj { get; set; }
    public string NomeEmpresarial { get; set; }
    public string NomeFantasia { get; set; }
    public string Situacao { get; set; }
    public string Abertura { get; set; }
    public string Tipo { get; set; }
    public string NaturezaJuridica { get; set; }
    public string AtividadePrincipal { get; set; }

    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Municipio { get; set; }
    public string Uf { get; set; }
    public string Cep { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
