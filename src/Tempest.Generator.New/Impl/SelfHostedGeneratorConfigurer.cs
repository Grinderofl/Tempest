using System;
using Tempest.Core.Operations;
using Tempest.Core.Scaffolding;

namespace Tempest.Generator.New.Impl
{
    public class SelfHostedGeneratorConfigurer : NewGeneratorConfigurerBase
    {
        public SelfHostedGeneratorConfigurer(NewGeneratorOptions generatorOptions) : base(generatorOptions)
        {
        }




        protected override void CopyGeneratorFiles(IScaffoldBuilder builder)
        {
            base.CopyGeneratorFiles(builder);

            // Copy project.json (with self-host enabled)
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("SelfHosted/project.json")).ToFile("Project.json");
            // Copy Program.cs
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("SelfHosted/Program.cs")).ToFile("Program.cs");

        }

        protected override bool ShouldExecuteScaffolder()
        {
            // Alternative way to do this:
            // Do default stuff in Generator, and supplement this additional configurer as

            return GeneratorOptions.SelfHosted;
        }
    }
}