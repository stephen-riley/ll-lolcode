namespace LolCode.Ast
{
    public class TypeNode : AstNode
    {
        public VarTypes Value { get; protected set; }

        public TypeNode(VarTypes val)
        {
            Value = val;
        }

        public override void Emit()
        {
            throw new System.NotImplementedException();
        }

        public override VarTypes GetLolType() => Value;
    }
}