using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Rx.NetSequenceBasics
{
    public static class RxAggregation
    {
        public static void Dump<T>(this IObservable<T> source, string name)
        {
            source.Subscribe(
            i => Console.WriteLine("{0}-->{1}", name, i),
            ex => Console.WriteLine("{0} failed-->{1}", name, ex.Message),
            () => Console.WriteLine("{0} completed", name));
        }

        static void Main(string[] args)
        {
            //CountMethod();
            //MinMaxSumAvg();
            //ScanMethod();
            GroupByMethod();
            Console.ReadKey();
        }

        static void CountMethod()
        {
            var numbers = Observable.Range(0, 10);
            numbers.Dump("numbers");
            numbers.Count().Dump("count");
        }

        static void MinMaxSumAvg()
        {
            var numbers = new Subject<int>();
            numbers.Dump("numbers");
            numbers.Min().Dump("Min");
            numbers.Max().Dump("Max");
            numbers.Sum().Dump("Sum");
            numbers.Average().Dump("Average");
            numbers.OnNext(6);
            numbers.OnNext(2);
            numbers.OnNext(1);
            numbers.OnNext(7);
            numbers.OnNext(3);
            numbers.OnCompleted();
        }

        static void ScanMethod()
        {
            var numbers = new Subject<int>();
            var scan = numbers.Scan(0, (acc, current) => acc + current);
            numbers.Dump("numbers");
            scan.Dump("scan");
            numbers.OnNext(1);
            numbers.OnNext(2);
            numbers.OnNext(3);
            numbers.OnCompleted();
        }

        static void GroupByMethod()
        {
            var source = Observable.Interval(TimeSpan.FromSeconds(0.1)).Take(10);
            var group = source.GroupBy(i => i % 3);
            group.SelectMany(
            grp =>
            grp.Max()
            .Select(value => new { grp.Key, value }))
            .Dump("group");
        }
    }
}
