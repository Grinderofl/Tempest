using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core;

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

            protected override void ExecuteCore()
            {
                Create.FromString("Foo").ToStream(_streamAction);
            }
        }
    }
}
