using System;
using LolCode.Compiler;

namespace LolCode.Ast
{
    public class StringNode : AstNode
    {
        public string Value { get; protected set; }

        public int StringTableIndex { get; set; }

        public StringNode(string val)
        {
            Value = val;
        }

        public override void Emit()
        {
            var r = Lolc.NextReg;
            var len = Value.Length + 1;
            Console.WriteLine($"    %{r} = getelementptr inbounds [{len} x i8], [{len} x i8]* @.str{StringTableIndex}, i64 0, i64 0");
        }

        public override VarTypes GetLolType() => VarTypes.LolString;
    }
}