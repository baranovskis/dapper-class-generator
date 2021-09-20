using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using CommandLine;
using DapperClassGenerator.Templates;

namespace DapperClassGenerator
{
    class Program
    {
        private class Options
        {
            [Option('c', "connection", Required = true, HelpText = "Set MSSQL connection string.")]
            public string ConnectionString { get; set; }

            [Option('n', "namespace", Required = true, HelpText = "Set class namespace.")]
            public string Namespace { get; set; }

            [Option('o', "output", HelpText = "Generated file path.")]
            public string OutputPath { get; set; }

            [Option("annotations", HelpText = "Use Dapper.Contrib data annotations.")]
            public bool UseDataAnnotations { get; set; }

            [Option("validator", HelpText = "Generate validator class (FluentValidation).")]
            public bool GenerateModelValidator { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    Console.WriteLine("Generation started!");

                    var sw = Stopwatch.StartNew();

                    var data = DbReader.ReadSchema(o.ConnectionString);

                    // This variable type should be an interface because avoid CS1061 compile error.
                    // At runtime, the implementation's TransformText() is called.
                    var model = new ModelTemplate(o.Namespace, o.UseDataAnnotations, data)
                        .TransformText();

                    var modelValidator = string.Empty;

                    if (o.GenerateModelValidator)
                    {
                        modelValidator = new ModelValidatorTemplate(o.Namespace, data)
                            .TransformText();
                    }

                    // If file path is provided, then save the output to the file.
                    if (!string.IsNullOrEmpty(o.OutputPath))
                    {
                        File.WriteAllText(Path.Combine(o.OutputPath, "GeneratedClass.cs"), model,
                            Encoding.UTF8);

                        if (o.GenerateModelValidator)
                        {
                            File.WriteAllText(Path.Combine(o.OutputPath, "GeneratedValidatorClass.cs"), 
                                modelValidator, Encoding.UTF8);
                        }
                    }
                    else
                    {
                        // Display generated templates output
                        Console.WriteLine(model);
                        Console.WriteLine();
                        Console.WriteLine(modelValidator);
                    }

                    sw.Stop();

                    Console.WriteLine("POCOs class generation completed!");
                    Console.WriteLine($"Time taken: {sw.Elapsed.ToString()}");
                });
        }
    }
}