using System;
using System.Collections.Generic;
using Tempest.Core;
using Tempest.Core.Options;
using Tempest.Core.Generator;

namespace Tempest.Generator.Zing
{
    public class ZingGenerator : GeneratorBase
    {
        private string _fooName;

        protected override IEnumerable<ConfigurationOption> SetupOptions()
        {
            yield return
                Options.Input("Welcome to new Zing generator! \n" +
                              "Please enter the name for your Zing", 
                              s => _fooName = s);

        }

        protected override void ExecuteCore()
        {
            Set.TargetSubDirectory(_fooName);
            Globally.TransformToken("Bar", _fooName);

            Copy.Resource("Zing.Template.rename.me").ToFile($"{_fooName}Foo.bar");
        }
    }
}
