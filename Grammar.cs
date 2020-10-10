using Sprache;

namespace LolCode.Ast
{
    public class Grammar
    {
        public static Parser<AstNode> STRING =
            from openQuote in Parse.Char('\'')
            from str in Parse.Regex(@"[^']*")
            from closeQuote in Parse.Char('\'')
            select AstNode.AsString(str);

        public static Parser<AstNode> ID =
            from id in Parse.Regex(@"[A-Za-z_\-0-9\.]+").Token().Text()
            select AstNode.AsIdentifier(id);

        public static Parser<AstNode> FLOAT =
            from value in Parse.DecimalInvariant
            select AstNode.AsFloat(double.Parse(value));

        public static Parser<AstNode> INT =
            from value in Parse.Number
            select AstNode.AsInt(int.Parse(value));

        public static Parser<AstNode> NUMBAR =
            from lolType in Parse.String("NUMBAR").Token()
            select AstNode.AsType(lolType.ToString());

        public static Parser<AstNode> NUMBR =
            from lolType in Parse.String("NUMBR").Token()
            select AstNode.AsType(lolType.ToString());

        public static Parser<AstNode> YARN =
            from lolType in Parse.String("YARN").Token()
            select AstNode.AsType(lolType.ToString());

        public static Parser<AstNode> LolType =
            NUMBAR
            .Or(NUMBR)
            .Or(YARN);

        public static Parser<AstNode> Atom =
            STRING
            .Or(FLOAT)
            .Or(INT);

        public static Parser<AstNode> VarDecl =
            from x in Parse.String("I HAZ A").Token()
            from lolType in LolType
            from y in Parse.String("ITZ").Token()
            from id in ID
            select AstNode.AsVarDecl(id, lolType);

        public static Parser<AstNode> Assignment =
            from x in Parse.String("LOL").Token()
            from id in ID
            from y in Parse.String("R").Token()
            from expr in Expression
            select AstNode.AsAssignment(id, expr);

        public static Parser<AstNode> Statement =
            Assignment
            .Or(VarDecl);

        public static Parser<AstNode> Expression =
            from atom in Atom
            select atom;

        public static Parser<AstNode> ProgramStart =
            from literal in Parse.String("HAI").Token()
            select AstNode.AsToken(NodeTypes.ProgramStart, literal.ToString());

        public static Parser<AstNode> ProgramEnd =
            from literal in Parse.String("KTHXBYE").Token()
            select AstNode.AsToken(NodeTypes.ProgramEnd, literal.ToString());

        public static Parser<AstNode> Program =
           from progStart in ProgramStart
           from stats in Statement.AtLeastOnce()
           from progEnd in ProgramEnd
           select AstNode.AsBody(stats);
    }
}