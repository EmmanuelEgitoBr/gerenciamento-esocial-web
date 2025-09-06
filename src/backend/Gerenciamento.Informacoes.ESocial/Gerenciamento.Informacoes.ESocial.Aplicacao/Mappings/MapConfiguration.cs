using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Mappings;

public class MapConfiguration
{
    public static MapperConfiguration RegisterMap()
    {
        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<Arquivo, ArquivoDto>().ReverseMap();
        }
        );
        return mapperConfiguration;
    }
}
