using System;
using System.Threading;

namespace FlaUILibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FlaUILibrary HTTP keyword server starting...");
            var server = new FlaUILibraryServer(prefix: "http://localhost:5000/");
            server.Start();

            Console.WriteLine("Press Ctrl+C to stop the server.");
            var exitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (s, e) => { e.Cancel = true; exitEvent.Set(); };
            exitEvent.WaitOne();

            server.Stop();
            Console.WriteLine("FlaUILibrary server stopped.");
        }
    }
}
