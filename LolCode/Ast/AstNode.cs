using System;
using System.Collections.Generic;
using LolCode.Compiler;

namespace LolCode.Ast
{
    public abstract class AstNode
    {
        public AstNode Parent { get; protected set; }

        public IList<AstNode> Children { get; protected set; } = new List<AstNode>();

        public Lolc Lolc { get; set; }

        public abstract void Emit();

        public abstract VarTypes GetLolType();

        public AstNode AssignParents(Lolc compiler, AstNode parent = null)
        {
            Parent = parent;
            Lolc = compiler;

            foreach (var child in Children)
            {
                child.AssignParents(compiler, this);
            }

            return this;
        }

        public BodyNode GetScope()
        {
            var node = this;
            do
            {
                if (node is BodyNode body)
                {
                    return body;
                }

                node = node.Parent;
            } while (node != null);

            return null;
        }
    }
}