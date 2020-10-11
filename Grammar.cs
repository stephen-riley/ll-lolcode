using Sprache;

namespace LolCode.Ast
{
    public class Grammar
    {
        public static Parser<AstNode> STRING =
            from openQuote in Parse.Char('\'')
            from str in Parse.Regex(@"[^']*")
            from closeQuote in Parse.Char('\'')
            select new StringNode(str);

        public static Parser<AstNode> FLOAT =
            from value in Parse.DecimalInvariant
            select new FloatNode(double.Parse(value));

        public static Parser<AstNode> INT =
            from value in Parse.Number
            select new IntNode(int.Parse(value));

        public static Parser<IdentifierNode> ID =
            from id in Parse.Regex(@"[A-Za-z_\-0-9\.]+").Token().Text()
            select new IdentifierNode(id);

        public static Parser<TypeNode> NUMBAR =
            from lolType in Parse.String("NUMBAR").Token().Text()
            select new TypeNode(lolType);

        public static Parser<TypeNode> NUMBR =
            from lolType in Parse.String("NUMBR").Token().Text()
            select new TypeNode(lolType);

        public static Parser<TypeNode> YARN =
            from lolType in Parse.String("YARN").Token().Text()
            select new TypeNode(lolType);

        public static Parser<TypeNode> LolType =
            NUMBAR
            .Or(NUMBR)
            .Or(YARN);

        public static Parser<AstNode> Atom =
            STRING
            .Or(FLOAT)
            .Or(INT);

        public static Parser<VarDeclNode> VarDecl =
            from x in Parse.String("I HAZ A").Token()
            from lolType in LolType
            from y in Parse.String("ITZ").Token()
            from id in ID
            select new VarDeclNode(id.Identifier, lolType);

        public static Parser<AssignmentNode> Assignment =
            from x in Parse.String("LOL").Token()
            from id in ID
            from y in Parse.String("R").Token()
            from expr in Expression
            select new AssignmentNode(id.Identifier, expr);

        public static Parser<AstNode> Statement =
            (Parser<AstNode>)Assignment
            .Or((Parser<AstNode>)VarDecl);

        public static Parser<AstNode> Expression =
            from atom in Atom
            select atom;

        public static Parser<AstNode> ProgramStart =
            from literal in Parse.String("HAI").Token()
            select default(AstNode);

        public static Parser<AstNode> ProgramEnd =
            from literal in Parse.String("KTHXBYE").Token()
            select default(AstNode);

        public static Parser<AstNode> Program =
           from progStart in ProgramStart
           from stats in Statement.AtLeastOnce()
           from progEnd in ProgramEnd
           select new BodyNode(stats);
    }
}