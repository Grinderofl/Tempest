using System;
using System.Collections.Generic;
using Tempest.Core;
using Tempest.Core.Options;

namespace MyProj
{
    public class MyProjGenerator : GeneratorBase
    {
        private string _fooName;

        protected override IEnumerable<ConfigurationOption> SetupOptions()
        {
            yield return
                Options.Input("Welcome to new Foo generator! \n " +
                              "Please enter the name for your Foo", 
                              s => _fooName = s);

        }

        protected override void ExecuteCore()
        {
            Set.TargetSubDirectory(_fooName);
            Globally.TransformToken("Bar", _fooName);

            Copy.Resource("MyProj.Template.rename.me").ToFile($"{_fooName}Foo.bar");
        }
    }
}
