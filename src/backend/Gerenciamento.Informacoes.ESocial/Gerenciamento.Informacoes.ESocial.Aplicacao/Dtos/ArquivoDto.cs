﻿namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Dtos;

public class ArquivoDto
{
    public int ArquivoId { get; set; }
    public int? TrabalhadorId { get; set; }
    public string? Tipo { get; set; }
    public string? NomeArquivo { get; set; }
}
