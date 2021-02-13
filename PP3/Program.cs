using System;
using System.Threading;
using System.Threading.Tasks;
using PP3.DatabaseContext;

namespace PP3
{
    class Program
    {
        static void Main(string[] args)
        {
            //RetrieveDataFromOriginalRepo();
            //RetrieveDataFromFixedRepo();
            OriginalDeleteDataWithFK(2);
            //FixedDeleteDataWithFK(2);
        }

        static void RetrieveDataFromOriginalRepo()
        {
            var messageRepo = new MessageRepo();

            for (int i = 0; i < 100; i++)
            {
                var thread = new Thread(() =>
                {
                    Console.WriteLine($"Tried getting the data from thread: {Thread.CurrentThread.ManagedThreadId}");
                    var message = messageRepo.GetById(1);
                    Console.WriteLine($"Got the data from thread: {Thread.CurrentThread.ManagedThreadId}");
                });
                thread.Start();
            }
        }

        static void RetrieveDataFromFixedRepo()
        {
            var messageRepo = new FixedMessageRepo(50);
            for (int i = 0; i < 100; i++)
            {
                var thread = new Thread(() =>
                {
                    Console.WriteLine($"Tried getting the data from thread: {Thread.CurrentThread.ManagedThreadId}");
                    var message = messageRepo.GetById(1);
                    Console.WriteLine($"Got the data from thread: {Thread.CurrentThread.ManagedThreadId}");
                });
                thread.Start();
            }
        }

        static void OriginalDeleteDataWithFK(int id)
        {
            var messageRepo = new MessageRepo();
            Task.Run(() => messageRepo.DeleteMessageExtension(2));
            messageRepo.DeleteMessage(2);
        }

        static void FixedDeleteDataWithFK(int id)
        {
            var messageRepo = new FixedMessageRepo(1);
            var manualResetEvent = new ManualResetEvent(false);
            Task.Run(() => messageRepo.DeleteMessageExtension(2, () => manualResetEvent.Set()));
            manualResetEvent.WaitOne();
            messageRepo.DeleteMessage(2);
        }

    }
}
