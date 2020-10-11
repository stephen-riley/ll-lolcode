using System.Collections.Generic;

namespace LolCode.Ast
{
    public class BodyNode : AstNode
    {
        public BodyNode(IEnumerable<AstNode> statements)
        {
            Children = new List<AstNode>(statements);
        }

        public override void Emit()
        {
            foreach (var child in Children)
            {
                child.Emit();
            }
        }
    }
}