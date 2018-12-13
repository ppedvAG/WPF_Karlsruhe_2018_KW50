using System;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hallo TPL");

            //Parallel.Invoke(Zählen, Zählen, Zählen, Zählen, Zählen, Zählen, Zählen, Zählen, Zählen);
            //Parallel.For(0, 1000000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));

            var t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(800);
                Console.WriteLine("T1 fertig");
                throw new FieldAccessException();
            });

            var t2 = new Task<long>(() =>
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(1600);
                Console.WriteLine("T2 fertig");
                return 743874873487;
            });

            t1.ContinueWith(tex =>
            {
                Console.WriteLine($"Fehler: {tex.Exception.InnerException.Message}");
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);


            var tw = Task.WhenAll(t1, t2).ContinueWith(t =>
            {
                Console.WriteLine($"Beide OK: {t2.Result}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);


            t1.Start();
            t2.Start();



            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        private static void Zählen()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
