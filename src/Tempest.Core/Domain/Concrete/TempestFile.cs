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

        public string Filename { get; protected set; }
        public string Directory { get; protected set; }
        public string FilePath { get; protected set; }
        public Stream Contents => new FileStream(FilePath, FileMode.OpenOrCreate);
        
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
    }
}
