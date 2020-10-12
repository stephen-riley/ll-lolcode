using System;
using LolCode.Compiler;

namespace LolCode.Ast
{
    public class PrintNode : AstNode
    {
        public bool ShouldPrintNewline { get; protected set; }

        public PrintNode(AstNode expression, bool noNewLine = false)
        {
            Children.Add(expression);
            ShouldPrintNewline = !noNewLine;
        }

        public override void Emit()
        {
            var expr = Children[0];
            expr.Emit();

            var lolType = expr.GetLolType();

            switch (lolType)
            {
                case VarTypes.LolString:
                    EmitString();
                    break;
                case VarTypes.LolInt:
                    EmitInt();
                    break;
                case VarTypes.LolFloat:
                    EmitFloat();
                    break;
                default:
                    throw new Exception("could not print expression of unkonwn type");
            }

            if (ShouldPrintNewline)
            {
                Lolc.Out.WriteLine(@"
    call i32 (i8 *, ...) @printf(i8* getelementptr inbounds ([2 x i8], [2 x i8]* @.nl, i64 0, i64 0))");
            }
        }

        private void EmitString()
        {
            if (Children[0] is StringNode strNode)
            {
                Lolc.Out.WriteLine($@"
    call i32 (i8 *, ...) @printf(i8* getelementptr inbounds ([{strNode.Value.Length + 1} x i8], [{strNode.Value.Length + 1} x i8]* @.str{strNode.StringTableIndex}, i64 0, i64 0))");
            }
            else if (Children[0] is IdentifierNode identNode)
            {
                var r = Lolc.NextReg;
                var llvmType = Lolc.GetLlvmType(identNode);
                var alignment = Lolc.GetLlvmTypeAlignment(identNode);

                Lolc.Out.WriteLine($"    %{r} = load {llvmType}, {llvmType}* %{identNode.Identifier}, align {alignment}");
                Lolc.Out.WriteLine($@"
    call i32 (i8 *, ...) @printf(i8* %{r})");
            }
            else
            {
                throw new Exception($"invalid node type for EmitString(): {Children[0].GetType().ToString()}");
            }
        }

        private void EmitInt()
        {
            if (Children[0] is IntNode intNode)
            {
                var r = Lolc.NextReg;
                Lolc.Out.WriteLine($"    %{r} = add i32, 0, {intNode.Value}");
                Lolc.Out.WriteLine($@"
    call i32 (i8 *, ...) @printf(i8* getelementptr inbounds ([3 x i8], [3 x i8]* @.percent_d, i64 0, i64 0), i32 %{r})");
            }
            else if (Children[0] is IdentifierNode identNode)
            {
                var r = Lolc.NextReg;
                Lolc.Out.WriteLine($"    %{r} = load i32, i32* %{identNode.Identifier}, align 4");
                Lolc.Out.WriteLine($@"
    call i32 (i8 *, ...) @printf(i8* getelementptr inbounds ([3 x i8], [3 x i8]* @.percent_d, i64 0, i64 0), i32 %{r})");
            }
            else
            {
                throw new Exception($"invalid node type for EmitString(): {Children[0].GetType().ToString()}");
            }
        }

        private void EmitFloat()
        {
            if (Children[0] is FloatNode floatNode)
            {
                var r = Lolc.NextReg;
                Lolc.Out.WriteLine($"    %{r} = add double, 0, {floatNode.Value}");
                Lolc.Out.WriteLine($@"
    call i32 (i8 *, ...) @printf(i8* getelementptr inbounds ([3 x i8], [3 x i8]* @.percent_f, i64 0, i64 0), double %{r})");
            }
            else if (Children[0] is IdentifierNode identNode)
            {
                var r = Lolc.NextReg;
                Lolc.Out.WriteLine($"    %{r} = load double, double* %{identNode.Identifier}, align 8");
                Lolc.Out.WriteLine($@"
    call i32 (i8 *, ...) @printf(i8* getelementptr inbounds ([3 x i8], [3 x i8]* @.percent_f, i64 0, i64 0), i32 %{r})");
            }
            else
            {
                throw new Exception($"invalid node type for EmitString(): {Children[0].GetType().ToString()}");
            }
        }

        public override VarTypes GetLolType() => VarTypes.Unknown;
    }
}