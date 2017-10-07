using NetMQ;
using NetMQ.Sockets;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetMQDealerProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var end1 = new DealerSocket())
            {
                end1.Bind("tcp://*:5555");

                var end1Task = Task.Run(() =>
                {
                    Console.WriteLine("ThreadId = {0}", Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("Sending hello down the inproc pipeline");
                    end1.SendFrame("Hello");
                });
                Task.WaitAll(new[] { end1Task });
                //Thread.Sleep(500);
            }
            NetMQConfig.Cleanup();
            Console.ReadLine();
        }
    }
}
