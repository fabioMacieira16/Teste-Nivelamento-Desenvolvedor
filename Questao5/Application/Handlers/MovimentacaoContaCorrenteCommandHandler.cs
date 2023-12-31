﻿using AutoMapper;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Language;
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
                                                    , LogNotifications notifications
                                                    , IMapper mapper
                                                    , ILanguageSystem languageSystem
                                                     )
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _notifications = notifications ?? new LogNotifications();
        _movimentoRepository = movimentoRepository;
        _languageSystem = languageSystem;
        _mapper = mapper;
    }

    public async Task<MovimentarContaCorrenteResponse> Handle(MovimentacaoContaCorrenteCommand request, 
                                                              CancellationToken cancellationToken)
    {
        var resp = new MovimentarContaCorrenteResponse();

        var contaCorrente = await MovimentIsValid(request);
        if (_notifications.Any())
            return resp;

        var movimentoAdd = _mapper.Map<Movimento>(request);

        if (contaCorrente is not null)
        {

            movimentoAdd.IdContaCorrente = contaCorrente.ContaCorrenteId;
            _movimentoRepository.Add(movimentoAdd);

        }

        await _movimentoRepository.UnitOfWork.CommitAsync();
        return resp;
    }

    async Task<ContaCorrente?> MovimentIsValid(MovimentacaoContaCorrenteCommand request)
    {

        string[] tipoMovimentoValid = { "C", "D" };

        if (request.Valor <= 0)
            _notifications.Add(new Notifications { Message = _languageSystem.InvalidValue() });

        var numeroConta = request.Numero;

        ContaCorrente? contaCorrente = await CurrentAccountIsValid(numeroConta);

        if (!tipoMovimentoValid.Any(x => x == request.TipoMovimento))
            _notifications.Add(new Notifications { Message = _languageSystem.InvalidType() });

        return contaCorrente;
    }

    public async Task<ObterSaldoContaCorrenteDto> Handle(ObterSaldoContaCorrenteQuery request,
                                                         CancellationToken cancellationToken)
    {
        var resp = new ObterSaldoContaCorrenteDto();
        var contaCorrente = await CurrentAccountIsValid(request.Numero);
        if (_notifications.Any())
            return resp;

        var sqlCredito = @" 
                            SELECT 
                            coalesce(SUM(VALOR),0)
                            FROM
                            contacorrente CC JOIN 
                            movimento MV ON MV.idcontacorrente = CC.idcontacorrente
                            WHERE tipomovimento = 'C'
                            AND CC.numero = @conta
                        ";

        var sqlDebito = @" 
                            SELECT 
                            coalesce(SUM(VALOR),0)
                            FROM
                            contacorrente CC JOIN 
                            movimento MV ON MV.idcontacorrente = CC.idcontacorrente
                            WHERE tipomovimento = 'D'
                            AND CC.numero = @conta
                        ";
        

        var valorCredito = await _movimentoRepository.RepositoryConsult.GetOneAsync<decimal>(sqlCredito, new { conta = request.Numero });
        var valorDebito = await _movimentoRepository.RepositoryConsult.GetOneAsync<decimal>(sqlDebito, new { conta = request.Numero });


        resp = _mapper.Map<ObterSaldoContaCorrenteDto>(contaCorrente);
        resp.Saldo = String.Format("{0:C}", (valorCredito + (valorDebito * -1)));
        return resp;
    }

    async Task<ContaCorrente?> CurrentAccountIsValid(int numeroConta)
    {
        var contaCorrente = (await _contaCorrenteRepository.RepositoryConsult.SearchAsync(x => x.Numero == numeroConta, true))
                              ?.FirstOrDefault();

        if (contaCorrente is null)
            _notifications.Add(new Notifications { Message = _languageSystem.InvalidAccount() });

        if (contaCorrente is not null
                          && !contaCorrente.Ativo)
            _notifications.Add(new Notifications { Message = _languageSystem.InactiveAccount() });
        return contaCorrente;
    }

}