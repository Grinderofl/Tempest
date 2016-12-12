using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core.Domain;
using Tempest.Core.Persistence.Impl;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.Core.IntegrationTests.Persistence
{
    public class FileSystemTests
    {
        public abstract class FileSystemFixture : IDisposable
        {
            protected FileSystemFixture()
            {
                FileSystem = new FileSystem();
            }

            protected FileSystem FileSystem;

            private readonly List<string> _createdFiles = new List<string>();

            protected virtual void CreateFileWithText(string file, string text)
            {
                _createdFiles.Add(file);
                File.WriteAllText(file, text);
            }

            public void Dispose()
            {
                foreach(var file in _createdFiles)
                    File.Delete(file);
            }
        }

        public class WhenOpeningExistingFile : FileSystemFixture
        {
            public WhenOpeningExistingFile()
            {
                CreateFileWithText("foo.txt", "Foo");
            }
            [Fact]
            public void finds_file()
            {
                var file = FileSystem.Open("foo.txt");
                Assert.Equal("foo.txt", file.Filename);
            }

            [Fact]
            public void gets_contents()
            {
                var file = FileSystem.Open("foo.txt");
                Assert.Equal("Foo", file.Contents.ReadAsString());
            }
        }

        public class WhenOpeningInvalidFile : FileSystemFixture
        {
            [Fact]
            public void throws_exception()
            {
                Func<IFile> fileDelegate = () => FileSystem.Open("bar.txt");

                Assert.Throws<FileNotFoundException>(fileDelegate);
            }
        }
    }
}
