namespace LolCode.Ast
{
    public class FloatNode : AstNode
    {
        public double Value { get; protected set; }

        public FloatNode(double val)
        {
            Value = val;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }
    }
}