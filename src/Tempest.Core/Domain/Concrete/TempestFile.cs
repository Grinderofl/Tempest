using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.Core.Domain.Concrete
{
    public class TempestFile : IFile
    {
        public TempestFile(string filePath)
        {
            UpdateFilepath(filePath);
        }

        //private Stream _stream;

        public string Filename { get; protected set; }
        public string Directory { get; protected set; }
        public string FilePath { get; protected set; }
        public byte[] Contents { get; set; }

        public virtual bool Exists() => File.Exists(FilePath);
        public void UpdateFilename(string newName)
        {
            Filename = newName;
            FilePath = Path.Combine(Directory, Filename);
        }

        public void UpdateFilepath(string newPath)
        {
            var directory = Path.GetDirectoryName(newPath);
            var filename = Path.GetFileName(newPath);
            Directory = directory;
            Filename = filename;
            FilePath = Path.Combine(Directory, Filename);
        }

        public IFile Load()
        {
            if(!Exists())
                throw new FileNotFoundException($"Unable to load the file '{FilePath}'");

            Contents = File.ReadAllBytes(FilePath);
            return this;
        }
        
    }

    public class FilePath
    {
        public string Filename { get; }
        public string Directory { get; }

        public string GetFullPath() => Path.Combine(Directory, Filename);
    }
}
