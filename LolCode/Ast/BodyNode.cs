using System.Collections.Generic;
using LolCode.Compiler;

namespace LolCode.Ast
{
    public class BodyNode : AstNode
    {
        public IDictionary<string, VarTypes> SymbolTable { get; private set; } = new Dictionary<string, VarTypes>();

        public BodyNode(IEnumerable<AstNode> statements)
        {
            Children = new List<AstNode>(statements);
        }

        public override void Emit()
        {
            Children.Apply(c => c.Emit());
        }

        public override VarTypes GetLolType() => VarTypes.Unknown;
    }
}