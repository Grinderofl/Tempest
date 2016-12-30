using Tempest.Core.Operations;
using Tempest.Core.Scaffolding;

namespace Tempest.Generator.New.Impl
{
    public class SelfHostedGeneratorConfigurer : NewGeneratorConfigurerBase
    {
        public SelfHostedGeneratorConfigurer(NewGeneratorOptions generatorOptions) : base(generatorOptions)
        {
        }

        public override int Order => 1;
        protected override void ConfigureScaffolder(IScaffoldBuilder scaffolder)
        {
            // Copy project.json (with self-host enabled)
            // Copy Program.cs
            // Copy HelloWorldGenerator.cs

            // Copy Template/Program.cs
            // Copy Template/project.json
            // Copy Template/ReplaceMeGreeter.cs


            // Copy build.cake (with dotnetpublish)
            // Copy build.ps1


        }
    }
}