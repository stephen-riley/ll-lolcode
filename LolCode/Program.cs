using System;
using LolCode.Ast;
using Sprache;

namespace LolCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = @"HAI
                I HAZ A NUMBR ITZ BOB
                LOL BOB R 1
            KTHXBYE
            ";

            string code2 = "I HAZ A NUMBR ITZ BOB";

            string code3 = "LOL BOB R 1";

            var parser = Grammar.Program;
            var prog = parser.Parse(code);

            Console.WriteLine("Hello World!");
        }
    }
}
