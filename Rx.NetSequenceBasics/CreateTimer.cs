using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Rx.NetSequenceBasics
{
    class CreateTimer
    {
        static void Main(string[] args)
        {
            NonBlocking_event_driven();
            //UnRegisterdEventHandler();
            Console.ReadKey();
        }
        static void NonBlocking_event_driven()
        {
            //var ob = Observable.Create<string>(
            //observer =>
            //{
            //    var timer = new System.Timers.Timer();
            //    timer.Interval = 1000;
            //    timer.Elapsed += (s, e) => observer.OnNext("tick");
            //    timer.Elapsed += OnTimerElapsed;
            //    timer.Start();
            //    return Disposable.Empty;
            //});
            //var subscription = ob.Subscribe(Console.WriteLine);
            //Console.ReadLine();
            //subscription.Dispose();

            var ob = Observable.Create<string>(
            observer =>
            {
                var timer = new System.Timers.Timer();
                timer.Interval = 1000;
                timer.Elapsed += (s, e) => observer.OnNext("tick");
                timer.Elapsed += OnTimerElapsed;
                timer.Start();
                return timer;
            });
            var subscription = ob.Subscribe(Console.WriteLine);
            Console.ReadLine();
            subscription.Dispose();
        }

        static void UnRegisterdEventHandler()
        {
            var ob = Observable.Create<string>(
            observer =>
            {
                var timer = new System.Timers.Timer();
                timer.Enabled = true;
                timer.Interval = 1000;
                //timer.Elapsed += (s, e) => observer.OnNext("tick");
                timer.Elapsed += OnTimerElapsed;
                timer.Start();
                return () => {
                    timer.Elapsed -= OnTimerElapsed;
                    //Console.WriteLine();
                    timer.Dispose();
                };
            });
        }

       static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine(e.SignalTime);
        }
    }
}
