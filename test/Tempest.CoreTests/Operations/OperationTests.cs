﻿using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using Tempest.Core.Scaffolding.Operations;
using Tempest.Core.Scaffolding.Persistence;
using Tempest.Core.Scaffolding.Sources;
using Tempest.Core.Scaffolding.Transforms;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.CoreTests.Operations
{
    public class OperationTests
    {
        public abstract class OperationContext
        {
            protected virtual Operation CreateOperation(Action<Stream> emitterAction)
            {
                var streamFactory = CreateStreamFactory();
                var transformer = CreateTransformer();
                var emitter = CreatePersister(emitterAction);

                var operation = new Operation(streamFactory, transformer, emitter);
                return operation;
            }

            protected virtual IStreamSource CreateStreamFactory()
            {
                var streamFactory = new Mock<IStreamSource>();
                ModifyStreamFactory(streamFactory);
                return streamFactory.Object;
            }

            protected virtual void ModifyStreamFactory(Mock<IStreamSource> streamFactory)
            {
            }

            protected virtual IStreamTransformer CreateTransformer() => NoOpStreamTransformer.Instance;

            protected virtual IStreamPersister CreatePersister(Action<Stream> emitterAction)
            {
                var emitter = new Mock<IStreamPersister>();
                ModifyEmitter(emitterAction, emitter);
                return emitter.Object;
            }

            protected virtual void ModifyEmitter(Action<Stream> emitterAction, Mock<IStreamPersister> emitter)
            {
                emitter.Setup(x => x.Persist(It.IsAny<Stream>())).Callback(emitterAction);
            }
        }

        public class ExecutingMinimalOperation : OperationContext
        {
            protected override void ModifyStreamFactory(Mock<IStreamSource> streamFactory)
            {
                streamFactory.Setup(x => x.Create()).Returns("Foo".ToStream());
            }

            [Fact]
            public void executes_operation()
            {
                // Arrange
                var result = "";
                var operation = CreateOperation(s => result = s.ReadAsString());

                // Act
                operation.Execute();

                // Assert
                Assert.Equal("Foo", result);
            }
        }

        public class ExecutingSimpleTransformOperation : OperationContext
        {
            protected override void ModifyStreamFactory(Mock<IStreamSource> streamFactory)
            {
                streamFactory.Setup(x => x.Create()).Returns("Foo".ToStream());
            }

            protected override IStreamTransformer CreateTransformer()
            {
                return
                    ExpressionBasedStreamTransformer.Create(stream => stream.ReadAsString().Replace("o", "z").ToStream());
            }

            [Fact]
            public void executes_operation()
            {
                // Arrange
                var result = "";
                var operation = CreateOperation(s => result = s.ReadAsString());

                //Act
                operation.Execute();
                
                // Assert
                Assert.Equal("Fzz", result);
            }
        }

        public class ExecutingMultiTransformOperation : OperationContext
        {
            protected override void ModifyStreamFactory(Mock<IStreamSource> streamFactory)
            {
                streamFactory.Setup(x => x.Create()).Returns("Fizz".ToStream());
            }

            protected override IStreamTransformer CreateTransformer()
            {
                return new MultiTokenStreamTransformer(new Dictionary<string, string>()
                {
                    ["i"] = "u",
                    ["F"] = "B"
                });
            }

            [Fact]
            public void executes_opration()
            {
                var result = "";
                var operation = CreateOperation(s => result = s.ReadAsString());

                operation.Execute();

                Assert.Equal("Buzz", result);
            }
        }

        public class ExecutingCompoundTransformOperation : OperationContext
        {
            protected override void ModifyStreamFactory(Mock<IStreamSource> streamFactory)
            {
                streamFactory.Setup(x => x.Create()).Returns("Fizz".ToStream());
            }


            protected override IStreamTransformer CreateTransformer()
            {
                return new CompoundStreamTransformer(new List<IStreamTransformer>()
                {
                    new TokenStreamTransformer("i", "u"),
                    new TokenStreamTransformer("F", "B")
                });
            }

            [Fact]
            public void executes_opration()
            {
                var result = "";
                var operation = CreateOperation(s => result = s.ReadAsString());

                operation.Execute();

                Assert.Equal("Buzz", result);
            }
        }
    }
}