using System.IO;
using LolCode.Ast;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sprache;

namespace LolCode.Tests
{
    [TestClass]
    public class ParseTests
    {
        [TestMethod]
        public void ParsePreamblePermutations()
        {
            var ast1 = Grammar.ProgramStart.Parse("HAI");
            var ast2 = Grammar.ProgramStart.Parse("O HAI");
        }

        [TestMethod]
        public void ParseOptionalFooter()
        {
            var ast1 = Grammar.Program.Parse("HAI I SEZ \"\" KTHXBYE");
            var ast2 = Grammar.Program.Parse("HAI I SEZ \"\"");
        }

        [TestMethod]
        public void ParseString()
        {
            var ast1 = Grammar.STRING.Parse("\"a string\"");
        }

        [TestMethod]
        public void ParseHelloWorld()
        {
            var src = File.ReadAllText("fixtures/helloworld.lol");
            var ast = Grammar.Program.Parse(src);
            Assert.IsNotNull(ast);
        }
    }
}
