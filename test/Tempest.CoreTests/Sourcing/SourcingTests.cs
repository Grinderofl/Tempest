﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Tempest.Core.Sourcing;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.CoreTests.Sourcing
{
    public class SourcingTests
    {
        public class StringContentSourceTests
        {
            [Fact]
            public void generates_valid_stream()
            {
                var generator = new StringContentSource("TestContent");

                var result = generator.Generate(new SourcingContext());

                var resultValue = result.First().OutputStream.ReadAsString();
                Assert.Equal("TestContent", resultValue);
            }
        }

        public class TemplateFileSourceTests : IDisposable
        {
            public TemplateFileSourceTests()
            {
                File.WriteAllText("foo.bar", "Foobar");
            }
            [Fact]
            public void generates_valid_stream()
            {
                var source = BuildTemplateSourceLocation();
                var context = new SourcingContext()
                {
                    TemplateRoot = new DirectoryInfo(Directory.GetCurrentDirectory())
                };

                var result = source.Generate(context);
                var resultValue = result.First().OutputStream.ReadAsString();
                Assert.Equal("Foobar", resultValue);
            }

            private static TemplateFileSource BuildTemplateSourceLocation()
            {
                var generator = new TemplateFileSource("foo.bar");
                return generator;
            }

            public void Dispose()
            {
                File.Delete("foo.bar");
            }
        }

        public class ResourceSourceTests
        {
            [Fact]
            public void generates_valid_stream()
            {
                var source = new ResourceFileSource("Tempest.CoreTests.Sourcing.EmbeddedResource.txt",
                    typeof(TemplateFileSourceTests).GetTypeInfo().Assembly);
                var context = new SourcingContext();
                var result = source.Generate(context);
                var resultValue = result.First().OutputStream.ReadAsString();
                Assert.Equal("FOOBAR", resultValue);
            }
        }
    }
}
