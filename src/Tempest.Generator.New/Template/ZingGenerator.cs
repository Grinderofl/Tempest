using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Generator;
using Tempest.Core.Scaffolding;

namespace Tempest.Generator.Zing
{
    public class ZingGenerator : GeneratorBase
    {
        private string _fooName;


        protected override void ConfigureOptions(OptionsFactory options)
        {
            options.Input("Welcome to new Zing generator! \n" +
                          "Please enter the name for your Zing",
                          s => _fooName = s);
        }

        protected override void ConfigureGenerator(IScaffoldBuilder scaffoldBuilder)
        {
            scaffoldBuilder.Set.TargetSubDirectory(_fooName);
            scaffoldBuilder.Globally.TransformToken("Bar", _fooName);

            scaffoldBuilder.Copy.ResourceOf<ZingGenerator>("Zing.Template.rename.me").ToFile($"{_fooName}Foo.bar");
        }
    }
}
