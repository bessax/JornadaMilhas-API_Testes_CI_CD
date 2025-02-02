﻿using JornadaMilhas.Dominio.Validacao;
using JornadaMilhas.Dominio.ValueObjects;

namespace JornadaMilhas.Dominio.Entidades;

public class OfertaViagem : Valida
{
    public int Id { get; set; }
    public virtual Rota Rota { get; set; } 
    public Periodo Periodo { get; set; } // ValueObject (DDD)
    public double Preco { get; set; }
    public OfertaViagem()
    {
            
    }
    public OfertaViagem(Rota rota, Periodo periodo, double preco)
    {
        Rota = rota;
        Periodo = periodo;
        Preco = preco;
        Validar();
    }

    public override string ToString()
    {
        return $"Origem: {Rota.Origem}, Destino: {Rota.Destino}, Data de Ida: {Periodo.DataInicial.ToShortDateString()}, Data de Volta: {Periodo.DataFinal.ToShortDateString()}, Preço: {Preco:C}";
    }

    protected override void Validar()
    {
        if (Periodo.EhValido)
        {
            Erros.RegistrarErro(Periodo.Erros.Sumario);
        }
        else if (Rota == null || Periodo == null)
        {
            Erros.RegistrarErro("A oferta de viagem não possui rota ou período válidos.");
        }
        else if (Preco <= 0)
        {
            Erros.RegistrarErro("O preço da oferta de viagem deve ser maior que zero.");
        }
    }
}
