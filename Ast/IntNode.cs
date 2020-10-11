namespace LolCode.Ast
{
    public class IntNode : AstNode
    {
        public int Value { get; protected set; }

        public IntNode(int val)
        {
            Value = val;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}