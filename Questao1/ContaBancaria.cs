using System;

namespace Questao1;

class ContaBancaria
{
    private int numeroConta;
    private string titularConta;
    private double saldo;

    public ContaBancaria(int numeroConta, string titularConta, double depositoInicial = 0.0)
    {
        this.numeroConta = numeroConta;
        this.titularConta = titularConta;
        this.saldo = depositoInicial;
    }

    public int NumeroConta
    {
        get { return numeroConta; }
    }

    public string TitularConta
    {
        get { return titularConta; }
        set { titularConta = value; }
    }

    public double Saldo
    {
        get { return saldo; }
    }

    public void Deposito(double valor)
    {
        saldo += valor;
    }

    public void Saque(double valor)
    {
        if (valor <= saldo)
        {
            saldo -= valor;
            saldo -= 3.50; // Taxa de saque
        }
        else
        {
            Console.WriteLine("Saldo insuficiente.");
        }
    }

    public override string ToString()
    {
        return $"Conta {numeroConta}, Titular: {titularConta}, Saldo: $ {saldo.ToString("F2")}";
    }
}
