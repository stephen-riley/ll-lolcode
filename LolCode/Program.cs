using System;
using System.IO;
using LolCode.Ast;
using LolCode.Compiler;
using Sprache;

namespace LolCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string source;

            if (args.Length > 0)
            {
                source = File.ReadAllText(args[0]);
            }
            else
            {
                source = @"
                HAI
                    I HAZ A YARN ITZ BOB
                    LOL BOB R ""hello, world!""
                    I SEZ BOB
                KTHXBYE
            ";
            }

            new Lolc().CompileSource(source);
        }
    }
}
