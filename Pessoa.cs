using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleFinanceiro
{
    public class Pessoa
    {
        private static int contadorId = 1;
        public int Id { get; }
        public string Nome { get; }
        public int Idade { get; }
        private readonly List<Transacao> transacoes = new List<Transacao>();



        public Pessoa(string nome, int idade)
        {
            Id = contadorId++;
            Nome = nome;
            Idade = idade;
            transacoes = new List<Transacao>();
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            transacoes.Add(transacao);
        }

        public List<Transacao> GetTransacoes()
        {
            return transacoes;
        }
    }
}