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
            throw new System.NotImplementedException();
        }
    }
}