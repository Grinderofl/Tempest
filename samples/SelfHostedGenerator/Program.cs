using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Boot.Runner.Activation.Impl;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Generator;
using Tempest.Core.Utils;

namespace SelfHostedGenerator
{
    /// <summary>
    /// This sample demonstrates a self-hosted (no tempest frontend) generator.
    /// This generator can independently packaged into an executable, ready for
    /// running without the arguments like generator name etc.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new GeneratorContext()
            {
                Arguments = args,
                GeneratorType = typeof(HelloWorldGenerator),
                TempestDirectory = typeof(HelloWorldGenerator).GetAssembly().GetAssemblyDirectory(),
                WorkingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()),
                TemplateDirectory =  typeof(HelloWorldGenerator).GetAssembly().GetAssemblyDirectory().GetDirectories("Template").First()
            };

            new GeneratorBootstrapperFactory().Create(context).Execute(new GeneratorExecutor());
            Console.WriteLine("Completed");
        }
    }
}
