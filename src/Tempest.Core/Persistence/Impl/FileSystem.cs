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
        /// <summary>
        /// Opens a file. 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IFile Open(string path)
        {
            var file = new TempestFile(path);
            if(!file.Exists())
                throw new FileNotFoundException($"The file '{path}' could not be found.");
            return file;
        }

        public void Write(IFile file)
        {
            file.Stream.Position = 0;
            var data = new byte[file.Stream.Length];
            file.Stream.Read(data, 0, (int)file.Stream.Length);
            File.WriteAllBytes(file.FilePath, data);
            file.Stream.Dispose();
        }

        // Filesystem
        // Has entries
        //
        // FileSystemEntry
        // 
    }

    
}
