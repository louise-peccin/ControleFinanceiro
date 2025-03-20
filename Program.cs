using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleFinanceiro
{
    static public class Program
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();

        static void Main()
        {
            int op = 0;

            while (op != 4)
            {
                Console.WriteLine("\n======= MENU =======");
                Console.WriteLine("1 - CADASTRAR PESSOA");
                Console.WriteLine("2 - CADASTRAR TRANSAÇÃO");
                Console.WriteLine("3 - LISTAR TOTAIS");
                Console.WriteLine("4 - DELETAR PESSOA");
                Console.WriteLine("5 - SAIR");

                if (!int.TryParse(Leitura.EntDados("\nEscolha uma opção: "), out op))
                {
                    Console.WriteLine("Entrada inválida! Digite um número.");
                    continue;
                }

                switch (op)
                {
                    case 1:
                        CadastrarPessoa();
                        break;
                    case 2:
                        CadastrarTransacao();
                        break;
                    case 3:
                        ListarTotais();
                        break;
                    case 4:
                        DeletarPessoa();
                        break;
                    case 5:
                        Console.WriteLine("\nEncerrando o programa...\n");
                        return;
                    default:
                        Console.WriteLine("\nOpção inválida!!");
                        break;
                }
            }
        }

        static void CadastrarPessoa()
        {
            string nome = Leitura.EntDados("\nNome: ");

            if (!int.TryParse(Leitura.EntDados("Idade: "), out int idade) || idade < 0)
            {
                Console.WriteLine("Idade inválida! Deve ser um número inteiro positivo.");
                return;
            }

            Pessoa novaPessoa = new Pessoa(nome, idade);
            pessoas.Add(novaPessoa);
            Console.WriteLine($"Pessoa cadastrada com sucesso! ID: {novaPessoa.Id}");
        }

        static void CadastrarTransacao()
        {
            if (!int.TryParse(Leitura.EntDados("ID da pessoa: "), out int idPessoa))
            {
                Console.WriteLine("ID inválido! Digite um número.");
                return;
            }

            Pessoa? pessoa = pessoas.FirstOrDefault(p => p.Id == idPessoa);

            if (pessoa == null)
            {
                Console.WriteLine("Pessoa não encontrada!");
                return;
            }

            string descricao = Leitura.EntDados("Descrição: ");

            if (!double.TryParse(Leitura.EntDados("Valor: "), out double valor) || valor <= 0)
            {
                Console.WriteLine("O valor deve ser um número positivo!");
                return;
            }

            string tipo = pessoa.Idade < 18 ? "Despesa" : Leitura.EntDados("Tipo (Receita/Despesa): ");

            if (!tipo.Equals("Receita", StringComparison.OrdinalIgnoreCase) &&
                !tipo.Equals("Despesa", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Tipo inválido! Escolha 'Receita' ou 'Despesa'.");
                return;
            }

            pessoa.AdicionarTransacao(new Transacao(descricao, valor, tipo));
            Console.WriteLine("Transação cadastrada com sucesso!");
        }

        static void ListarTotais()
        {
            if (pessoas.Count == 0)
            {
                Console.WriteLine("\nNenhuma pessoa cadastrada.");
                return;
            }

            double totalReceitas = 0;
            double totalDespesas = 0;

            Console.WriteLine("\nResumo de Gastos:");

            foreach (var pessoa in pessoas)
            {
                double receitas = pessoa.GetTransacoes()
                    .Where(t => t.Tipo.Equals("Receita", StringComparison.OrdinalIgnoreCase))
                    .Sum(t => t.Valor);

                double despesas = pessoa.GetTransacoes()
                    .Where(t => t.Tipo.Equals("Despesa", StringComparison.OrdinalIgnoreCase))
                    .Sum(t => t.Valor);

                totalReceitas += receitas;
                totalDespesas += despesas;

                Console.WriteLine($"{pessoa.Nome} -> Receitas: R$ {receitas:F2} | Despesas: R$ {despesas:F2} | Saldo: R$ {receitas - despesas:F2}");
            }

            Console.WriteLine($"\nTOTAL GERAL -> Receitas: R$ {totalReceitas:F2} | Despesas: R$ {totalDespesas:F2} | Saldo: R$ {totalReceitas - totalDespesas:F2}");
        }

        static void DeletarPessoa()
        {
            if (!int.TryParse(Leitura.EntDados("ID da pessoa a deletar: "), out int idPessoa))
            {
                Console.WriteLine("ID inválido! Digite um número.");
                return;
            }

            Pessoa? pessoa = pessoas.FirstOrDefault(p => p.Id == idPessoa);

            if (pessoa == null)
            {
                Console.WriteLine("Pessoa não encontrada!");
                return;
            }

            pessoas.Remove(pessoa);
            Console.WriteLine($"Pessoa {pessoa.Nome} e todas as suas transações foram removidas!");
        }
    }
}

