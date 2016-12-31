//using System;
//using System.Collections.Generic;
//using System.IO;
//using Tempest.Core.Persistence.Impl;
//using Tempest.Core.Utils;
//using Xunit;

//namespace Tempest.Core.IntegrationTests.Persistence
//{
//    public class FileSystemTests
//    {
//        public abstract class FileSystemFixture : IDisposable
//        {
//            protected FileSystemFixture()
//            {
//                FileSystem = new FileSystem();
//            }

//            protected FileSystem FileSystem;

//            private readonly List<string> _createdFiles = new List<string>();

//            protected virtual void CreateFileWithText(string file, string text)
//            {
//                AddFileToDelete(file);
//                File.WriteAllText(file, text);
//            }

//            protected virtual void AddFileToDelete(string file)
//            {
//                _createdFiles.Add(file);
//            }

//            public void Dispose()
//            {
//                foreach(var file in _createdFiles)
//                    File.Delete(file);
//            }
//        }

//        //public class WhenSavingNewFile : FileSystemFixture
//        //{
//        //    public WhenSavingNewFile()
//        //    {
//        //        AddFileToDelete("foo.bar");
//        //    }

//        //    [Fact]
//        //    public void saves_file()
//        //    {
//        //        var file = new TempestFile("foo.bar");
//        //        var textToCopy = "foobar".ToStream();
//        //        textToCopy.CopyTo(file.Stream);
//        //        Assert.True(File.Exists("foo.bar"));
//        //    }

//        //}

//        //public class WhenOpeningExistingFile : FileSystemFixture
//        //{
//        //    public WhenOpeningExistingFile()
//        //    {
//        //        CreateFileWithText("foo.txt", "Foo");
//        //    }
//        //    [Fact]
//        //    public void finds_file()
//        //    {
//        //        var file = FileSystem.Open("foo.txt");
//        //        Assert.Equal("foo.txt", file.Filename);
//        //    }

//        //    [Fact]
//        //    public void gets_contents()
//        //    {
//        //        var file = FileSystem.Open("foo.txt");
//        //        Assert.Equal("Foo", file.Stream.ReadAsString());
//        //    }
//        //}

//        //public class WhenOpeningInvalidFile : FileSystemFixture
//        //{
//        //    [Fact]
//        //    public void throws_exception()
//        //    {
//        //        Func<IFile> fileDelegate = () => FileSystem.Open("bar.txt");

//        //        Assert.Throws<FileNotFoundException>(fileDelegate);
//        //    }
//        //}
//    }
//}
