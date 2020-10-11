namespace LolCode.Ast
{
    public class AssignmentNode : AstNode
    {
        public string Identifier { get; protected set; }

        public AstNode Expression { get; protected set; }

        public AssignmentNode(string identifier, AstNode expression)
        {
            Identifier = identifier;
            Expression = expression;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}