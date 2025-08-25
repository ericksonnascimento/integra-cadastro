namespace IntegraCadastro.Api.Models;

public class Recebe
{
    public string CnpjDestino { get; set; }
    public string CnpjEmpresa { get; set; }
    public string CnpjOrigem { get; set; }
    public int CodFuncao { get; set; }
    public string DataGeracao { get; set; }
    public string Nire { get; set; }
    public string Protocolo { get; set; }
    public string Servico { get; set; }
    public object Json { get; set; } 
    public string Xml { get; set; }
}