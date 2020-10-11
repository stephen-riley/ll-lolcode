namespace LolCode.Ast
{
    public class TypeNode : AstNode
    {
        public string Value { get; protected set; }

        public TypeNode(string val)
        {
            Value = val;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}