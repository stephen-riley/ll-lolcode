namespace LolCode.Ast
{
    public class StringNode : AstNode
    {
        public string Value { get; protected set; }

        public StringNode(string val)
        {
            Value = val;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}