using Sprache;

namespace LolCode.Ast
{
    public class Grammar
    {
        public static Parser<AstNode> STRING =
            from _1 in Parse.Char('\"')
            from str in Parse.Regex(@"[^""]*")
            from _2 in Parse.Char('\"')
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
            from _ in Parse.String("NUMBAR").Token()
            select new TypeNode(VarTypes.LolFloat);

        public static Parser<TypeNode> NUMBR =
            from _ in Parse.String("NUMBR").Token()
            select new TypeNode(VarTypes.LolInt);

        public static Parser<TypeNode> YARN =
            from _ in Parse.String("YARN").Token()
            select new TypeNode(VarTypes.LolString);

        public static Parser<TypeNode> LolType =
            NUMBAR
            .Or(NUMBR)
            .Or(YARN);

        public static Parser<AstNode> Atom =
            STRING
            .Or(INT)
            .Or(FLOAT)
            .Or(ID);

        public static Parser<AstNode> Expression =
            from atom in Atom
            select atom;

        public static Parser<VarDeclNode> VarDecl =
            from _1 in Parse.String("I HAZ A").Token()
            from lolType in LolType
            from _2 in Parse.String("ITZ").Token()
            from id in ID
            select new VarDeclNode(id.Identifier, lolType);

        public static Parser<AssignmentNode> Assignment =
            from _1 in Parse.String("LOL").Token()
            from id in ID
            from _2 in Parse.String("R").Token()
            from expr in Expression
            select new AssignmentNode(id.Identifier, expr);

        public static Parser<PrintNode> Print =
            from _ in Parse.String("I SEZ").Token().Text()
            from expr in Expression
            from excl in Parse.String("!").Optional()
            select new PrintNode(expr, excl == null);

        public static Parser<AstNode> Statement =
            (Parser<AstNode>)Assignment
            .Or((Parser<AstNode>)VarDecl)
            .Or((Parser<AstNode>)Print);

        public static Parser<AstNode> ProgramStart =
            from _1 in Parse.String("O").Token().Optional()
            from _2 in Parse.String("HAI").Token()
            select default(AstNode);

        public static Parser<AstNode> ProgramEnd =
            from _ in Parse.String("KTHXBYE").Token()
            select default(AstNode);

        public static Parser<AstNode> Program =
           from _1 in ProgramStart
           from stats in Statement.AtLeastOnce()
           from _2 in ProgramEnd.Optional()
           select new BodyNode(stats);
    }
}