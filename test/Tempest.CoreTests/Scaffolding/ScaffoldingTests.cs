using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Generator;
using Tempest.Core.Operations;
using Tempest.Core.Scaffolding;

namespace Tempest.CoreTests.Scaffolding
{
    public class ScaffoldingTests
    {
        private class TestGenerator : GeneratorBase
        {
            private readonly Stream _streamAction;

            public TestGenerator(Stream streamAction)
            {
                _streamAction = streamAction;
            }

            protected override void ConfigureOptions(OptionsFactory options)
            {
                throw new NotImplementedException();
            }

            protected override void ConfigureGenerator(ScaffolderConfigurer scaffold)
            {
                throw new NotImplementedException();
            }

            //protected override void ExecuteCore()
            //{
            //    Create.FromString("Foo").ToStream(_streamAction);
            //}
        }
    }
}
