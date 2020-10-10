namespace LolCode.Ast
{
    public enum NodeTypes : int
    {
        Undefined,

        Variable,

        Identifier,

        IntType,

        FloatType,

        StringType,

        FunctionCall,

        ProgramStart,

        ProgramEnd,

        LolType,

        VarDecl,

        Body,

        Assignment,
    }
}