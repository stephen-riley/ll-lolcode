using LolCode.Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LolCode.Tests
{
    [TestClass]
    public class CompilerPhaseTests
    {
        [TestMethod]
        public void BuildSimpleStringTable()
        {
            var cc = new Lolc();
            cc.CompileSource("HAI I SEZ \"hello\"");
            Assert.AreEqual(1, cc.StringTable.Count);
            Assert.AreEqual("hello", cc.StringTable[0]);
        }
    }
}
