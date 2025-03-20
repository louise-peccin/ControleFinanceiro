using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro
{
    public class Transacao
    {
        private static int contadorId = 0; 

        public int Id { get; }
        public string Descricao { get; }
        public double Valor { get; }
        public string Tipo { get; }

        public Transacao(string descricao, double valor, string tipo)
        {
            Id = ++contadorId; 
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
        }
    }
}