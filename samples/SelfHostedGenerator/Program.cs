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

namespace SelfHostedGenerator
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
            // Simplest scenario:
            // 1) Create a Generator Context
            // 2) Create Generator Bootstrapper
            // 3) Execute Bootstrapper

            var context = GeneratorContextFactory.Create<HelloWorldGenerator>(args);
            new GeneratorBootstrapperFactory().Create(context).Execute();
            Console.WriteLine("Completed");
        }
    }
}
