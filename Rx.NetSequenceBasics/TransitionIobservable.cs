using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.NetSequenceBasics
{
    class TransitionIobservable
    {
        static void Main(string[] args)
        {
            //StartAction();
            //StartFunc();
            TaskToObservable();
            //APMBeginEnd();
            Console.ReadKey();
        }
        static void StartAction()
        {
            var start = Observable.Start(() =>
            {
                Console.Write("Working away");
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.Write(".");
                }
            });
            start.Subscribe(
            unit => Console.WriteLine("Unit published"),
            () => Console.WriteLine("Action completed"));
        }
        static void StartFunc()
        {
            var start = Observable.Start(() =>
            {
                Console.Write("Working away");
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(100);
                    Console.Write(".");
                }
                return "Published value";
            });
            start.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("Action completed"));
        }

        static void TaskToObservable()
        {
            var t = Task.Factory.StartNew(() => "Test");
            var source = t.ToObservable();
            source.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("completed"));
        }

        /*
        static void APMBeginEnd()
        {
            var fileLength = (int)Stream.Length;
            //read is a Func<byte[], int, int, IObservable<int>>
            var read = Observable.FromAsyncPattern<byte[], int, int, int>(
            IStream.BeginRead,
            stream.EndRead);
            var buffer = new byte[fileLength];
            var bytesReadStream = read(buffer, 0, fileLength);
            bytesReadStream.Subscribe(
            byteCount =>
            {
                Console.WriteLine("Number of bytes read={0}, buffer should be populated with data now.",
                byteCount);
            });
        }
        */
    }
}
