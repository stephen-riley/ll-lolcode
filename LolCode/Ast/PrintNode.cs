using System;

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
            if (Children[0] is StringNode strNode)
                Console.WriteLine($@"
    call i32 (i8 *, ...) @printf(i8* getelementptr inbounds ([{strNode.Value.Length + 1} x i8], [{strNode.Value.Length + 1} x i8]* @.str{strNode.StringTableIndex}, i64 0, i64 0))");

            if (ShouldPrintNewline)
            {
                Console.WriteLine(@"
    call i32 (i8 *, ...) @printf(i8* getelementptr inbounds ([2 x i8], [2 x i8]* @.nl, i64 0, i64 0))");
            }
        }
    }
}