﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Helpers;
using Tempest.Boot.Strappers.Defaults;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Conventions.Defaults;
using Tempest.Core.Generator;
using Tempest.Core.Scaffolding;
using Tempest.IntegrationTests.EndToEnd.Helpers;

namespace Tempest.IntegrationTests.EndToEnd.Base
{
    public abstract class ScaffoldingTestBase
    {
        private class TestGenerator : GeneratorBase
        {
            private readonly TestHelper _helper;

            public TestGenerator(TestHelper helper)
            {
                _helper = helper;
            }

            protected override void ConfigureOptions(OptionsFactory options)
            {

            }

            protected override void ConfigureGenerator(IScaffoldBuilder builder)
            {
                _helper.ScaffoldAction(builder);
            }
        }


        private class TestHelper
        {
            public Action<IScaffoldBuilder> ScaffoldAction { get; set; }
        }

        protected virtual GeneratorBootstrapper CreateBootstrapper(Action<IScaffoldBuilder> configurationAction)
        {
            var helper = new TestHelper { ScaffoldAction = configurationAction };

            var factory = new TestBootstrapperFactory(bootstrapper => bootstrapper.RegisterConvention(
                new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper))));
            return factory.Create(BootstrapperHelper.CreateTestContext<TestGenerator>());
        }

    }


}
