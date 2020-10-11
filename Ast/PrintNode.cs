namespace LolCode.Ast
{
    public class PrintNode : AstNode
    {
        public AstNode Expression { get; protected set; }

        public bool ShouldPrintNewline { get; protected set; }

        public PrintNode(AstNode expression, bool noNewLine = false)
        {
            Expression = expression;
            ShouldPrintNewline = !noNewLine;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}