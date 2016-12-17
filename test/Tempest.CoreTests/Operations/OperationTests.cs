using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Moq;
using Tempest.Core.Domain.Operations;
using Tempest.Core.Domain.Streaming;
using Tempest.Core.Emission;
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
                var emitter = CreateEmitter(emitterAction);

                var operation = new Operation(streamFactory, transformer, emitter);
                return operation;
            }

            protected virtual IStreamFactory CreateStreamFactory()
            {
                var streamFactory = new Mock<IStreamFactory>();
                ModifyStreamFactory(streamFactory);
                return streamFactory.Object;
            }

            protected virtual void ModifyStreamFactory(Mock<IStreamFactory> streamFactory)
            {
            }

            protected virtual Func<Stream, Stream> CreateTransformer()
            {
                return stream => stream;
            }

            protected virtual IStreamEmitter CreateEmitter(Action<Stream> emitterAction)
            {
                var emitter = new Mock<IStreamEmitter>();
                ModifyEmitter(emitterAction, emitter);
                return emitter.Object;
            }

            protected virtual void ModifyEmitter(Action<Stream> emitterAction, Mock<IStreamEmitter> emitter)
            {
                emitter.Setup(x => x.Emit(It.IsAny<Stream>())).Callback(emitterAction);
            }
        }

        public class ExecutingMinimalOperation : OperationContext
        {
            protected override void ModifyStreamFactory(Mock<IStreamFactory> streamFactory)
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

        public class ExecutingReplacingTransformOperation : OperationContext
        {
            protected override void ModifyStreamFactory(Mock<IStreamFactory> streamFactory)
            {
                streamFactory.Setup(x => x.Create()).Returns("Foo".ToStream());
            }

            protected override Func<Stream, Stream> CreateTransformer()
            {
                return stream => stream.ReadAsString().Replace("o", "z").ToStream();
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
    }
}
