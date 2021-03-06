﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Boot.Helpers;
using Tempest.Boot.Runner.Activation.Impl;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Generator;
using Tempest.Core.Utils;

namespace Tempest.Generator.HelloWorld
{
    /// <summary>
    /// This sample demonstrates a self-hosted (no tempest frontend) generator.
    /// This generator can independently packaged into an executable, ready for
    /// running without the arguments like generator name etc.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = GeneratorContextFactory.Create<HelloWorldGenerator>(args);
            new GeneratorBootstrapperFactory().Create(context).Execute();
            Console.WriteLine("Completed");
        }
    }
}
