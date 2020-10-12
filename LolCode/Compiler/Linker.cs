using System;
using System.IO;
using System.Threading.Tasks;
using CliWrap;

namespace LolCode.Compiler
{
    public class Linker
    {
        private static readonly string llvmPath = Environment.GetEnvironmentVariable("LLVM_DIRECTORY") ?? "/usr/local/opt/llvm/bin/";

        private static readonly string llvmAsPath = llvmPath + (llvmPath.EndsWith('/') ? "" : "/") + "llvm-as";
        private static readonly string llcPath = llvmPath + (llvmPath.EndsWith('/') ? "" : "/") + "llc";
        private static readonly string llvmGccPath = "/usr/bin/llvm-gcc";

        private string fileBasePath = Path.GetTempFileName();

        public async Task<string> Link(string llvmCode)
        {
            var llFile = fileBasePath + ".ll";
            var lcFile = fileBasePath + ".lc";
            var sFile = fileBasePath + ".s";
            var exeFile = fileBasePath;

            await File.WriteAllTextAsync(llFile, llvmCode);

            try
            {
                var result = await Cli.Wrap(llvmAsPath)
                    .WithArguments($"\"{llFile}\" -o \"{lcFile}\"")
                    .ExecuteAsync();
                result = await Cli.Wrap(llcPath)
                    .WithArguments($"\"{lcFile}\" -o \"{sFile}\"")
                    .ExecuteAsync();
                result = await Cli.Wrap(llvmGccPath)
                    .WithArguments($"-O3 -o \"{exeFile}\" \"{sFile}\"")
                    .ExecuteAsync();
            }
            catch (Exception e)
            {
                throw new Exception("failed to link llvm code", e);
            }

            return exeFile;
        }
    }
}