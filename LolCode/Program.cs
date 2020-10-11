using System;
using LolCode.Ast;
using Sprache;

namespace LolCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = @"
                HAI
                    I HAZ A NUMBR ITZ BOB
                    LOL BOB R 1
                KTHXBYE
            ";

            var parser = Grammar.Program;
            var prog = parser.Parse(code);

            Console.WriteLine("Hello World!");
        }
    }
}
