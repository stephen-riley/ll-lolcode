using System;
using System.IO;
using System.Threading.Tasks;
using LolCode.Compiler;

namespace LolCode
{
    class Program
    {
        static async Task Main(string[] args)
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

            var compiler = new Lolc();
            compiler.CompileSource(source);
            var exe = await new Linker().Link(compiler.Out.ToString());

            Console.WriteLine($"compiled to {exe}");
        }
    }
}
