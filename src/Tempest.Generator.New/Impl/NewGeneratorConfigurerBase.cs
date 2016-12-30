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
    }
}