using System.Collections.Generic;
using static LolCode.Ast.NodeTypes;

namespace LolCode.Ast
{
    public class AstNode
    {
        public NodeTypes Type { get; protected set; }

        public string Text { get; protected set; }

        public int Int { get; protected set; }

        public double Float { get; protected set; }

        public IList<AstNode> Children { get; protected set; }

        protected AstNode() { }

        public AstNode(NodeTypes type, string id = null, int intVal = 0, float floatVal = 0.0f)
        {
            Type = type;
            Text = id;
            Int = intVal;
            Float = floatVal;
        }

        public static AstNode AsVariable(string id) => new AstNode { Type = Variable, Text = id };
        public static AstNode AsInt(int intVal) => new AstNode { Type = IntType, Int = intVal };
        public static AstNode AsFloat(double floatVal) => new AstNode { Type = FloatType, Float = floatVal };
        public static AstNode AsString(string stringVal) => new AstNode { Type = StringType, Text = stringVal };
        public static AstNode AsIdentifier(string id) => new AstNode { Type = Identifier, Text = id };
        public static AstNode AsToken(NodeTypes type, string literal = "") => new AstNode { Type = type, Text = literal };
        public static AstNode AsType(string lolType) => new AstNode { Type = LolType, Text = lolType };
        public static AstNode AsVarDecl(AstNode id, AstNode lolType) => new AstNode { Type = VarDecl, Children = new List<AstNode> { id, lolType } };
        public static AstNode AsBody(IEnumerable<AstNode> statements) => new AstNode { Type = Body, Children = new List<AstNode>(statements) };
        public static AstNode AsAssignment(AstNode id, AstNode expr) => new AstNode { Type = Assignment, Text = id.Text, Children = new List<AstNode> { expr } };
    }
}