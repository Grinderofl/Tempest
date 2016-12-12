using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core.Domain;
using Tempest.Core.Domain.Concrete;

namespace Tempest.Core.Persistence.Impl
{
    public class FileSystem : IFileSystem
    {
        public IFile Open(string path)
        {
            var file = new TempestFile(path);
            if(!file.Exists())
                throw new FileNotFoundException($"The file '{path}' could not be found.");
            return file;
        }

        public void Write(IFile file)
        {
            file.Contents.Position = 0;
            var data = new byte[file.Contents.Length];
            file.Contents.Read(data, 0, (int)file.Contents.Length);

            File.WriteAllBytes(file.FilePath, data);
        }
    }
}
