namespace LolCode.Ast
{
    public class IdentifierNode : AstNode
    {
        public string Identifier { get; protected set; }

        public IdentifierNode(string identifier)
        {
            Identifier = identifier;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}