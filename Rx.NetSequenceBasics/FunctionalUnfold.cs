using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rx.NetSequenceBasics
{
    class FunctionalUnfold
    {
        static void Main(string[] args)
        {
            //Corecursion();
            //Range(5,8);
            //Interval();
            //Timer();
            GenerateTimer();
            Console.ReadKey();
        }
        private static IEnumerable<T> Unfold<T>(T seed, Func<T, T> accumulator)
        {
            var nextValue = seed;
            while (true)
            {
                yield return nextValue;
                nextValue = accumulator(nextValue);
            }
        }

        static void Corecursion()
        {
            var naturalNumbers = Unfold(1, i => i + 1);
            Console.WriteLine("1st 10 Natural numbers");
            foreach (var naturalNumber in naturalNumbers.Take(10))
            {
                Console.WriteLine(naturalNumber);
            }

        }

        static void Range()
        {
            var range = Observable.Range(10, 15);
            range.Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
        }

        public static IObservable<int> Range(int start, int count)
        {
            var max = start + count;
            return Observable.Generate(
            start,
            value => value < max,
            value => value + 1,
            value => value);
        }

        static void Interval()
        {
            var interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
            interval.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("completed"));
        }

        static void Timer()
        {
            //var interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
            //interval.Subscribe(Console.WriteLine);
            var timer = Observable.Timer(TimeSpan.FromSeconds(10));
            timer.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("completed"));
        }

        static void GenerateTimer()
        {
            var obs = Observable.Generate(
                0,
                _ => true,
                i => i + 1,
                i => new string('*', i),
                i => TimeSpan.FromSeconds(i)
                );
            //obs.Subscribe(x => Console.WriteLine(x));
            using (obs.Subscribe(Console.WriteLine))
            {
                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();
            }
        }
    }
}
