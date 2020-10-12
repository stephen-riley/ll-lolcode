using System;
using LolCode.Compiler;

namespace LolCode.Ast
{
    public class VarDeclNode : AstNode
    {
        public string Identifier { get; protected set; }

        public TypeNode TypeExpression { get; protected set; }

        public VarDeclNode(string identifier, TypeNode expression)
        {
            Identifier = identifier;
            TypeExpression = expression;
        }

        public override void Emit()
        {
            var lolType = TypeExpression.GetLolType();

            switch (lolType)
            {
                case VarTypes.LolInt:
                    Lolc.Out.WriteLine($"    %{Identifier} = alloca i32, align 4");
                    break;
                case VarTypes.LolFloat:
                    Lolc.Out.WriteLine($"    %{Identifier} = alloca double, align 8");
                    break;
                case VarTypes.LolString:
                    Lolc.Out.WriteLine($"    %{Identifier} = alloca i8*, align 8");
                    break;
                default:
                    throw new Exception("cannot emit variable allocation for unknown type");
            }
        }

        public override VarTypes GetLolType() => VarTypes.Unknown;
    }
}