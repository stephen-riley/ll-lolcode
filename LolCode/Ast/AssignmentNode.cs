using System;
using LolCode.Compiler;

namespace LolCode.Ast
{
    public class AssignmentNode : AstNode
    {
        public string Identifier { get; protected set; }

        public AssignmentNode(string identifier, AstNode expression)
        {
            Identifier = identifier;
            Children.Add(expression);
        }

        public override void Emit()
        {
            Children[0].Emit();

            var curReg = Lolc.Reg;
            var llvmType = Lolc.GetLlvmType(Children[0]);
            var alignment = Lolc.GetLlvmTypeAlignment(Children[0]);

            Lolc.Out.WriteLine($"    store {llvmType} %{curReg}, {llvmType}* %{Identifier}, align {alignment}");
        }

        public override VarTypes GetLolType() => VarTypes.Unknown;
    }
}