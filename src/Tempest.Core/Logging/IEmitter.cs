using System;
using Tempest.Core.Operations;

namespace Tempest.Core.Logging
{
    public interface IEmitter
    {
        void SetForegroundColor(ConsoleColor color);
        void SetBackgroundColor(ConsoleColor color);
        void Emit(string line);
    }
}