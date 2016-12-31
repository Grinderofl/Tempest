using System;

namespace Tempest.Core.Logging.Impl
{
    public class Emitter : IEmitter
    {
        private ConsoleColor _activeForegroundColor;
        private ConsoleColor _activeBackgroundColor;

        public Emitter()
        {
            _activeForegroundColor = Console.ForegroundColor;
            _activeBackgroundColor = Console.BackgroundColor;
        }

        public void SetForegroundColor(ConsoleColor color)
        {
            _activeForegroundColor = color;
        }

        public void SetBackgroundColor(ConsoleColor color)
        {
            _activeBackgroundColor = color;
        }

        public void Emit(string line)
        {
            var origForeColor = Console.ForegroundColor;
            var origBackColor = Console.BackgroundColor;
            Console.ForegroundColor = _activeForegroundColor;
            Console.BackgroundColor = _activeBackgroundColor;
            Console.Write(line);
            Console.ForegroundColor = origForeColor;
            Console.BackgroundColor = origBackColor;
            Console.Write(Environment.NewLine);
        }
    }
}