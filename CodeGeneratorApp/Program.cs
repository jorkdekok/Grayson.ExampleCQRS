using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;

namespace CodeGeneratorApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
 
            await ReadModelGeneratorService.GenerateReadModel();

        }

    }
}
