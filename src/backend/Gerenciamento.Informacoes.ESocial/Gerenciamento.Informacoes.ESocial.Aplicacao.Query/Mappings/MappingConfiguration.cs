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
            #region Trabalhador

            config.CreateMap<Trabalhador, TrabalhadorDto>();

            config.CreateMap<TrabalhadorDto, Trabalhador>()
            .ForMember(dest => dest.Dependentes, opt => opt.Ignore())
            .ForMember(dest => dest.Estagiario, opt => opt.Ignore())
            .ForMember(dest => dest.Cedido, opt => opt.Ignore());

            #endregion

            #region Cedido

            config.CreateMap<Cedido, CedidoDto>();

            config.CreateMap<CedidoDto, Cedido>()
            .ForMember(dest => dest.Trabalhador, opt => opt.Ignore());

            #endregion

            #region Dependente

            config.CreateMap<Dependente, DependenteDto>();

            config.CreateMap<DependenteDto, Dependente>()
            .ForMember(dest => dest.Trabalhador, opt => opt.Ignore());

            #endregion

            #region Estagiario

            config.CreateMap<Estagiario, EstagiarioDto>();

            config.CreateMap<EstagiarioDto, Estagiario>()
            .ForMember(dest => dest.Trabalhador, opt => opt.Ignore());

            #endregion
        }
        );
        return mapperConfiguration;
    }
}
