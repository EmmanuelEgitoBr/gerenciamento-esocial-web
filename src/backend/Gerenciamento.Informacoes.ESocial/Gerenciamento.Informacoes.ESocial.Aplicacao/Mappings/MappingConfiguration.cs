﻿using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;
using Mpce.ECensoSocial.Domain.Domain.Entities;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Mappings;

public class MappingConfiguration
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Arquivo, ArquivoDto>().ReverseMap();
            config.CreateMap<Cedido, CedidoDto>().ReverseMap();
            config.CreateMap<Dependente, DependenteDto>().ReverseMap();
            config.CreateMap<Estagiario, EstagiarioDto>().ReverseMap();
            config.CreateMap<Trabalhador, TrabalhadorDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
