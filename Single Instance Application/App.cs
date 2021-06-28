using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingleInstanceApp
{
    public partial class App 
    {
        private const string eventName = "84bb9974-fb13-4927-bf47-91f9fca1601c";
        private EventWaitHandle singleInstanceEvent;

        protected void OnStartup()
        {
            bool created;
            singleInstanceEvent = new EventWaitHandle(false,
                                                      EventResetMode.AutoReset,
                                                      eventName,
                                                      out created);

            if (!created)
            {
                singleInstanceEvent.Set();
                // Shutdown();
            }
            else
            {
                SynchronizationContext ctx = SynchronizationContext.Current;
                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        singleInstanceEvent.WaitOne();
                        ctx.Post(_ => MakeActiveApplication(), null);
                    }
                });
            }
        }

        private void MakeActiveApplication()
        {
            Console.WriteLine("I am already running...");

            //MainWindow.Activate();
            //MainWindow.Topmost = true;
            //MainWindow.Topmost = false;
            //MainWindow.Focus();
        }
    }
}
