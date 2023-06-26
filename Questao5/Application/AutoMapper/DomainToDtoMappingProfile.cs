using AutoMapper;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.AutoMapper;
public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<ContaCorrente, ObterSaldoContaCorrenteDto>();
    }
}
