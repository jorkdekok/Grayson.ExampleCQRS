using System;
using System.Reflection;

namespace Grayson.MessagesMapper
{
    public class MessageMapper
    {
        public void CreateMessageMap()
        {
            // which messages?
            // .. commands using this convention: certain interface or naming convention
            // .. (domain) events this convention: certain interface or naming convention

            // find handlers?
            // .. using convention: certain interface or naming convention

            // find consumers?
            // .. using convention: certain interface or naming convention

            // create handle classes for each handler?

            // create consumer classes for each consumer? 
        }

        public void GenerateCode()
        {
            // Generate the actual source code
            //var code = GenerateDocumentStorageCode(mappings);

            var generator = new AssemblyGenerator();

            // Tell the generator which other assemblies that it should be referencing 
            // for the compilation
            generator.ReferenceAssembly(Assembly.GetExecutingAssembly());
            //generator.ReferenceAssemblyContainingType<NpgsqlConnection>();
            //generator.ReferenceAssemblyContainingType<QueryModel>();
            //generator.ReferenceAssemblyContainingType<DbCommand>();
            //generator.ReferenceAssemblyContainingType<Component>();

            //mappings.Select(x => x.DocumentType.Assembly).Distinct().Each(assem => generator.ReferenceAssembly(assem));

            // build the new assembly -- this will blow up if there are any
            // compilation errors with the list of errors and the actual code
            // as part of the exception message
            //var assembly = generator.Generate(code);
        }

        private string GenerateDocumentStorageCode(object mappings)
        {
            throw new NotImplementedException();
        }
    }
}
