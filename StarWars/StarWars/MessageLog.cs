using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars
{
    static class MessageLog
    {
        public static MessageProcessor processor = new MessageProcessor();

        public static void LoadDelegate()
        {
            var console_logger = new ConsoleMessageLogger();
            var file_logger = new FileMessageLogger("messages.log");
            processor.AddObserver(console_logger.LogMessage);
            processor.AddObserver(file_logger.AddMessage);
        }
    }

    internal class MessageProcessor
    {
        private Action<string> _ProcessMessageAction;

        public event Action<string> MessageReceived;

        public void ProcessMessage(string message)
        {
            _ProcessMessageAction?.Invoke(message);
            MessageReceived?.Invoke(message);
           
        }

        public void AddObserver(Action<string> ObserverMethod)
        {
            _ProcessMessageAction += ObserverMethod;
        }
    }

    internal class ConsoleMessageLogger
    {
        public void LogMessage(string message) =>
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()}\t:\t{message}");
    }

    internal class FileMessageLogger
    {
        private readonly string _FileName;

        public FileMessageLogger(string FileName) => _FileName = FileName;

        public void AddMessage(string msg)
        {
            using (var file = File.AppendText(_FileName))
                file.WriteLine($"{DateTime.Now}\t|\t{msg}");
        }
    }
}
