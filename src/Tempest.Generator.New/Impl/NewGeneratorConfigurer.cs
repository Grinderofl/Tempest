using System;
using Tempest.Core.Operations;
using Tempest.Core.Scaffolding;

namespace Tempest.Generator.New.Impl
{
    public class NewGeneratorConfigurer : NewGeneratorConfigurerBase
    {
        public NewGeneratorConfigurer(NewGeneratorOptions generatorOptions) : base(generatorOptions)
        {
        }

        protected override void CopyGeneratorFiles(IScaffoldBuilder builder)
        {
            base.CopyGeneratorFiles(builder);


            if (GeneratorOptions.SelfHosted)
                CopySelfHostedFiles(builder);
            else
                CopyLibraryFiles(builder);
        }

        private void CopyLibraryFiles(IScaffoldBuilder builder)
        {
            // Copy project.json (library enabled)
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("Library/project.json"))
                .ToFile(BuildTarget($"project.json"));
        }

        private void CopySelfHostedFiles(IScaffoldBuilder builder)
        {
            // Copy project.json (with self-host enabled)
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("SelfHosted/project.json"))
                .ToFile(BuildTarget("Project.json"));

            // Copy Program.cs
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("SelfHosted/Program.cs"))
                .ToFile(BuildTarget("Program.cs"));
        }
    }
}