using System.Collections.Generic;

namespace LolCode.Ast
{
    public abstract class AstNode
    {
        public IList<AstNode> Children { get; protected set; } = new List<AstNode>();

        public abstract void Emit();

        protected AstNode() { }
    }
}