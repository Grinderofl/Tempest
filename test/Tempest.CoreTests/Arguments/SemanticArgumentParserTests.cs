using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Boot;
using Tempest.Boot.Arguments.Impl;
//using Tempest.Boot.Arguments.Impl;
using Xunit;

namespace Tempest.CoreTests.Arguments
{
    public class SemanticArgumentParserTests
    {
        public class WhenParsingArguments
        {
            [Fact]
            public void parses_generator_with_semantic_arguments()
            {
                // Runs a generator 'new' with parameters 'MyGenerator nobuild conventional'
                var args = "new MyGenerator nobuild conventional".Split(' ');
                var parsedArgs = new SemanticArgumentParser().ParseArguments(args);
                Assert.Equal("-g", parsedArgs[0]);
                Assert.Equal("new", parsedArgs[1]);
                Assert.Equal("-p", parsedArgs[2]);
                Assert.Equal("MyGenerator nobuild conventional", parsedArgs[3]);
            }

            [Fact]
            public void parses_generator_with_parameter_arguments()
            {
                // Runs a generator 'MyGenerator' with search path "Some_Mock_Location" and verbosity Diagnostic
                var args = "-g MyGenerator -s \"Some_Mock_Location\" -v Diagnostic".Split(' ');
                var parsedArgs = new SemanticArgumentParser().ParseArguments(args);
                Assert.Equal("-g", parsedArgs[0]);
                Assert.Equal("MyGenerator", parsedArgs[1]);
                Assert.Equal("-s", parsedArgs[2]);
                Assert.Equal("\"Some_Mock_Location\"", parsedArgs[3]);
                Assert.Equal("-v", parsedArgs[4]);
                Assert.Equal("Diagnostic", parsedArgs[5]);
            }

            [Fact]
            public void parses_generator_from_semantic_with_parameters()
            {
                // Runs a generator 'new' with parameters 'MyGenerator nobuild conventional'
                // and search path 'My_Mock_Location' and verbosity Diagnostic
                var args = "new MyGenerator nobuild conventional -s \"My_Mock_Location\" -v Diagnostic".Split(' ');
                var parsedArgs = new SemanticArgumentParser().ParseArguments(args);
                Assert.Equal("-s", parsedArgs[0]);
                Assert.Equal("\"My_Mock_Location\"", parsedArgs[1]);
                Assert.Equal("-v", parsedArgs[2]);
                Assert.Equal("Diagnostic", parsedArgs[3]);
                Assert.Equal("-g", parsedArgs[4]);
                Assert.Equal("new", parsedArgs[5]);
                Assert.Equal("-p", parsedArgs[6]);
                Assert.Equal("MyGenerator nobuild conventional", parsedArgs[7]);

            }
        }
    }
}
