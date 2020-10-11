using System;

namespace LolCode.Ast
{
    public class IdentifierNode : AstNode
    {
        public string Identifier { get; protected set; }

        public IdentifierNode(string identifier)
        {
            Identifier = identifier;
        }

        public override void Emit()
        {
            // do nothing
        }

        public override VarTypes GetLolType()
        {
            var scope = GetScope();
            if (scope.SymbolTable.TryGetValue(Identifier, out var lolType))
            {
                return lolType;
            }
            else
            {
                throw new Exception($"could not find symbol {Identifier}");
            }
        }
    }
}