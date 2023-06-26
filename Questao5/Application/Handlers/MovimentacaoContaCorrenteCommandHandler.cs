using AutoMapper;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Repository;
using Questao5.Infrastructure.CrossCutting;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Application.Handlers;
public class MovimentacaoContaCorrenteCommandHandler 
    : IRequestHandler<MovimentacaoContaCorrenteCommand, MovimentarContaCorrenteResponse>,
    IRequestHandler<ObterSaldoContaCorrenteQuery, ObterSaldoContaCorrenteDto>
{

    readonly IBaseRepository<ContaCorrente> _contaCorrenteRepository;
    readonly IBaseRepository<Movimento> _movimentoRepository;
    readonly LogNotifications _notifications;
    readonly ILanguageSystem _languageSystem;
    readonly IMapper _mapper;

    public MovimentacaoContaCorrenteCommandHandler(IBaseRepository<ContaCorrente> contaCorrenteRepository
                                                    , IBaseRepository<Movimento> movimentoRepository
                                                    , LNotifications notifications
                                                    , IMapper mapper
                                                    , ILanguageSystem languageSystem
                                                     )
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _notifications = notifications ?? new LNotifications();
        _movimentoRepository = movimentoRepository;
        _languageSystem = languageSystem;
        _mapper = mapper;
    }
}