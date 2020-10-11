using System;
using LolCode.Ast;
using LolCode.Compiler;
using Sprache;

namespace LolCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = @"
                HAI
                    I SEZ ""hello, world!""
                KTHXBYE
            ";

            new Lolc().CompileSource(code);
        }
    }
}
