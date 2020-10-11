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
            throw new System.NotImplementedException();
        }
    }
}