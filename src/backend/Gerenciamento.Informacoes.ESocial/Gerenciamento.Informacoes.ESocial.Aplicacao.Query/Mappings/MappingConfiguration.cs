using AutoMapper;
using Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Dtos;
using Gerenciamento.Informacoes.ESocial.Dominio.Entidades;

namespace Gerenciamento.Informacoes.ESocial.Aplicacao.Query.Mappings;

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
