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
            throw new System.NotImplementedException();
        }
    }
}