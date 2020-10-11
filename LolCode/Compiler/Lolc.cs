using System;
using System.Collections.Generic;
using System.IO;
using LolCode.Ast;
using Sprache;

namespace LolCode.Compiler
{
    public class Lolc
    {
        private static int registerCounter = 1;

        public static int Reg = registerCounter;

        public static int NextReg => registerCounter++;

        public List<StringNode> StringTable { get; } = new List<StringNode>();

        public void CompileFile(string filename)
        {
            CompileSource(File.ReadAllText(filename));
        }

        public void CompileSource(string source)
        {
            var ast = Grammar.Program.Parse(source).AssignParents();
            BuildStringTable(ast);
            BuildSymbolTables(ast);
            EmitPreamble();
            EmitStrings();
            EmitFunctions();
            EmitMain();
            ast.Emit();
            EmitPostamble();
        }

        private void EmitPreamble()
        {
            Console.WriteLine(@"
; source_filename = ""TODO""

@.nl        = unnamed_addr constant [2 x i8] c""\0A\00"", align 1
@.percent_d = unnamed_addr constant [3 x i8] c""%d\00"", align 1
@.percent_f = unnamed_addr constant [3 x i8] c""%f\00"", align 1
@.percent_s = unnamed_addr constant [3 x i8] c""%s\00"", align 1
@.inputbuf  = global[1000 x i8] zeroinitializer
");
        }

        private void EmitMain()
        {
            Console.WriteLine(@"
define i32 @main() #0 {
");
        }

        private void EmitPostamble()
        {
            Console.WriteLine(@"
    ret i32 0
}

declare i32 @scanf(i8*, ...) #1
declare i32 @printf(i8*, ...) #1

attributes #0 = { noinline nounwind optnone ssp uwtable ""correctly-rounded-divide-sqrt-fp-math""=""false"" ""darwin-stkchk-strong-link"" ""disable-tail-calls""=""false"" ""frame-pointer""=""all"" ""less-precise-fpmad""=""false"" ""min-legal-vector-width""=""0"" ""no-infs-fp-math""=""false"" ""no-jump-tables""=""false"" ""no-nans-fp-math""=""false"" ""no-signed-zeros-fp-math""=""false"" ""no-trapping-math""=""false"" ""probe-stack""=""___chkstk_darwin"" ""stack-protector-buffer-size""=""8"" ""target-cpu""=""penryn"" ""target-features""=""+cx16,+cx8,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87"" ""unsafe-fp-math""=""false"" ""use-soft-float""=""false"" }
attributes #1 = { ""correctly-rounded-divide-sqrt-fp-math""=""false"" ""darwin-stkchk-strong-link"" ""disable-tail-calls""=""false"" ""frame-pointer""=""all"" ""less-precise-fpmad""=""false"" ""no-infs-fp-math""=""false"" ""no-nans-fp-math""=""false"" ""no-signed-zeros-fp-math""=""false"" ""no-trapping-math""=""false"" ""probe-stack""=""___chkstk_darwin"" ""stack-protector-buffer-size""=""8"" ""target-cpu""=""penryn"" ""target-features""=""+cx16,+cx8,+fxsr,+mmx,+sahf,+sse,+sse2,+sse3,+sse4.1,+ssse3,+x87"" ""unsafe-fp-math""=""false"" ""use-soft-float""=""false"" }
");
        }

        private void EmitStrings()
        {
            foreach (var s in StringTable)
            {
                Console.WriteLine($"@.str{s.StringTableIndex} = private unnamed_addr constant [{s.Value.Length + 1} x i8] c\"{s.Value}\\00\", align 1");
            }
        }

        private void EmitFunctions()
        {
        }

        private void BuildSymbolTables(AstNode node)
        {
            if (node is VarDeclNode declNode)
            {
                var scope = declNode.GetScope();
                var lolType = declNode.TypeExpression.Value;
                scope.SymbolTable[declNode.Identifier] = lolType;
            }
            else
            {
                node.Children.Apply(c => BuildSymbolTables(c));
            }
        }

        private void BuildStringTable(AstNode node)
        {
            if (node is StringNode strNode)
            {
                strNode.StringTableIndex = StringTable.Count;
                StringTable.Add(strNode);
            }
            else
            {
                node.Children.Apply(c => BuildStringTable(c));
            }
        }

        public static string GetLlvmType(AstNode node)
        {
            var lolType = node.GetLolType();
            return lolType switch
            {
                VarTypes.LolInt => "i32",
                VarTypes.LolFloat => "double",
                VarTypes.LolString => "i8*",
                _ => throw new Exception("cannot determine LLVM type for unknown LOL type")
            };
        }

        public static int GetLlvmTypeAlignment(AstNode node)
        {
            var lolType = node.GetLolType();
            return lolType switch
            {
                VarTypes.LolInt => 4,
                _ => 8
            };
        }
    }
}