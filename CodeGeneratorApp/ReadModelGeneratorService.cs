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
    public static class ReadModelGeneratorService
    {
        public static async Task GenerateReadModel()
        {
            MSBuildWorkspace workspace = MSBuildWorkspace.Create();
            Solution solution = await workspace.OpenSolutionAsync(@"C:\Sources\Repos\Grayson.ExampleCQRS\Grayson.ExampleCQRS.sln");

            var domainProject = solution.Projects.SingleOrDefault(p => p.Name == "Grayson.ExampleCQRS.Domain");
            if (domainProject != null)
            {
                //var node = CSharpSyntaxTree.ParseText(models).GetRoot();
                var domainDocuments = domainProject.Documents.Where(d => d.Folders.Contains("Model"));
                foreach (var document in domainDocuments)
                {
                    var node = await document.GetSyntaxRootAsync();
                    var readmodelNode = ReadModelGeneration.GenerateViewModel(node);
                    var sourceText = SourceText.From(readmodelNode.ToFullString());
                    var folders = new List<string>() { "Model" };

                    var newDoc = domainProject.AddDocument($"{document.Name.Replace("cs", "")}View.cs", sourceText, folders: folders);

                    if (workspace.TryApplyChanges(newDoc.Project.Solution))
                    {
                        Console.WriteLine($"Written {newDoc.Name}");
                }
            }
            }
            
            //var viewModel = ViewModelGeneration.GenerateViewModel(node);
            //if (viewModel != null)
            //{
                
            //    var currentProject = solution.Projects.Where(p => p.Name == "CodeGeneratorApp").Single();
            //    var sourceText = SourceText.From(viewModel.ToFullString());
            //    var folders = new List<string>() { "Model" };

            //    var newDoc = currentProject.AddDocument("GeneratedClass.cs", sourceText, folders: folders);

            //    if (workspace.TryApplyChanges(newDoc.Project.Solution))
            //    {
            //        Console.WriteLine($"Written {newDoc.Name}");
            //    }
            //}

            Console.ReadLine();
        }
    }
}
