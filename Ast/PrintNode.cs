namespace LolCode.Ast
{
    public class PrintNode : AstNode
    {
        public AstNode Expression { get; protected set; }

        public PrintNode(AstNode expression)
        {
            Expression = expression;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}