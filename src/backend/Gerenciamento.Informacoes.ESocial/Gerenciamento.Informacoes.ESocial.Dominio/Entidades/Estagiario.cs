using Gerenciamento.Informacoes.ESocial.Dominio.ObjetosValor;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gerenciamento.Informacoes.ESocial.Dominio.Entidades;

public class Estagiario
{
    [Key]
    public int EstagiarioId { get; set; }
    public int TrabalhadorId { get; set; }
    public int NaturezaEstagio { get; set; }
    public int? AreaAtuacaoId { get; set; }
    public string? RazaoSocialInstEnsino { get; set; }
    public string? CnpjInstEnsino { get; set; }
    public string? NomeSupervisor { get; set; }
    public Endereco? EnderecoInstEnsino { get; set; }
    public Trabalhador? Trabalhador { get; set; }
}