using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Rx.NetSequenceBasics
{
    class ReducingSequence
    {
        static void Main(string[] args)
        {
            //WhereFilter();
            //DistinctMethod();
            //DistinctUntillChange();
            IgnoreElementMethod();
            Console.ReadKey();
        }

        static void WhereFilter()
        {
            var oddNumbers = Observable.Range(0, 10)
                .Where(i => i % 2 == 0)
                .Subscribe(
                Console.WriteLine,
                () => Console.WriteLine("Completed"));
        }

        static void DistinctMethod()
        {
            var subject = new Subject<int>();
            var distinct = subject.Distinct();
            subject.Subscribe(
            i => Console.WriteLine("{0}", i),
            () => Console.WriteLine("subject.OnCompleted()"));
            distinct.Subscribe(
            i => Console.WriteLine("distinct.OnNext({0})", i),
            () => Console.WriteLine("distinct.OnCompleted()"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnCompleted();
        }

        static void DistinctUntillChange()
        {
            var subject = new Subject<int>();
            var distinct = subject.DistinctUntilChanged();
            subject.Subscribe(
            i => Console.WriteLine("{0}", i),
            () => Console.WriteLine("subject.OnCompleted()"));
            distinct.Subscribe(
            i => Console.WriteLine("distinct.OnNext({0})", i),
            () => Console.WriteLine("distinct.OnCompleted()"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(1);
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnCompleted();
        }

        static void IgnoreElementMethod()
        {
            var subject = new Subject<int>();
            //Could use subject.Where(_=>false);
            var noElements = subject.IgnoreElements();
            subject.Subscribe(
            i => Console.WriteLine("subject.OnNext({0})", i),
            () => Console.WriteLine("subject.OnCompleted()"));
            noElements.Subscribe(
            i => Console.WriteLine("noElements.OnNext({0})", i),
            () => Console.WriteLine("noElements.OnCompleted()"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnCompleted();
        }
    }
}
