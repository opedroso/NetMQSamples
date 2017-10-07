using NetMQ;
using NetMQ.Sockets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetMQDealerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var end2 = new DealerSocket())
            {
                end2.Connect("tcp://localhost:5555");

                var end2Task = Task.Run(() =>
                {
                    Console.WriteLine("ThreadId = {0}", Thread.CurrentThread.ManagedThreadId);
                    var message = end2.ReceiveFrameString();
                    Console.WriteLine(message);
                });
                Task.WaitAll(new[] { end2Task });
            }
            NetMQConfig.Cleanup();
            Console.ReadLine();
        }
    }
}
