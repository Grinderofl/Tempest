using System;
using System.IO;

namespace Tempest.Core.Domain
{
    // This could work like this:
    // 1) "Open" file
    // 2) Add transformers
    // 3) Write to file looping through transformers

    public interface IFile
    {
        string Filename { get; }
        string Directory { get; }
        string FilePath { get; }
        //byte[] Contents { get; set; }
        
        bool Exists();
        void UpdateFilename(string newName);
        void TransformUsing(Func<Stream, Stream> transformer);
        Stream GetTransformedStream();
    }
}
