using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleFinanceiro
{
    static class Leitura
    {
        public static string EntDados(string mensagem)
        {
            Console.Write(mensagem);
            return Console.ReadLine() ?? "";
        }
    }
}