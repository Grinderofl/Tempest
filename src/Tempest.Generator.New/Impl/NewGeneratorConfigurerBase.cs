using Tempest.Core.Scaffolding;

namespace Tempest.Generator.New.Impl
{
    public abstract class NewGeneratorConfigurerBase : AbstractScaffolderConfigurer
    {
        protected NewGeneratorOptions GeneratorOptions;

        protected NewGeneratorConfigurerBase(NewGeneratorOptions generatorOptions)
        {
            GeneratorOptions = generatorOptions;
        }

        protected abstract bool ShouldExecuteScaffolder();

        public override int Order => 0;

        protected virtual string ResourceFormat => "Tempest.Generator.New.Template.{0}";

        protected virtual string BuildResource(string s) => string.Format(ResourceFormat, s);

        protected virtual void CopyGeneratorFiles(IScaffoldBuilder builder)
        {
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("HelloWorldGenerator.cs")).ToFile("HelloWorldGenerator.cs");
        }

        protected virtual void CopyBuildScript(IScaffoldBuilder builder)
        {
            // Only supporting appveyor
            if (GeneratorOptions.BuildScriptTypes != BuildScriptTypes.AppVeyor) return;

            if(GeneratorOptions.SelfHosted)
                builder.Copy.ResourceOf<NewGenerator>(BuildResource("SelfHosted/build.cake")).ToFile("build.cake");
            else
                builder.Copy.ResourceOf<NewGenerator>(BuildResource("Library/build.cake")).ToFile("build.cake");
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("build.ps1")).ToFile("build.ps1");
        }

        protected virtual void CopyGeneratorTemplateFiles(IScaffoldBuilder builder)
        {
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("Template/Program.cs")).ToFile("Template/Program.cs");
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("Template/project.json")).ToFile("Template/project.json");
            builder.Copy.ResourceOf<NewGenerator>(BuildResource("Template/ReplaceMeGreeter.cs")).ToFiles();
        }

        protected override void ConfigureScaffolder(IScaffoldBuilder builder)
        {
            if (!ShouldExecuteScaffolder()) return;
            builder.Globally.TransformToken("ReplaceMe", GeneratorOptions.GeneratorName);
            builder.Set.TargetSubDirectory(GeneratorOptions.GeneratorName);

            CopyGeneratorFiles(builder);

            if(GeneratorOptions.IncludeBuildScript)
                CopyBuildScript(builder);

            CopyGeneratorTemplateFiles(builder);
        }
    }
}