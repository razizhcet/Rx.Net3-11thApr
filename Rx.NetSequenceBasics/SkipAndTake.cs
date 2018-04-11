using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Rx.NetSequenceBasics
{
    class SkipAndTake
    {
        static void Main(string[] args)
        {
            //SkipMethod();
            //TakeMethod();
            //SkipWhileMethod();
            //TakeWhileMethod();
            //SkipLastMethod();
            //TakeLastMethod();
            //SkipUntillMethod();
            TakeUntillMethod();
            Console.ReadKey();
        }
        static void SkipMethod()
        {
            Observable.Range(0, 10)
                .Skip(3)
                .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
        }

        static void TakeMethod()
        {
            Observable.Range(0, 10)
                .Take(3)
                .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
        }

        static void SkipWhileMethod()
        {
            var subject = new Subject<int>();
            subject
            .SkipWhile(i => i < 4)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);
            subject.OnNext(7);
            subject.OnNext(8);
            subject.OnCompleted();
        }

        static void TakeWhileMethod()
        {
            var subject = new Subject<int>();
            subject
            .TakeWhile(i => i < 4)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);
            subject.OnNext(7);
            subject.OnNext(8);
            subject.OnCompleted();
        }

        static void SkipLastMethod()
        {
            var subject = new Subject<int>();
            subject
            .SkipLast(2)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            Console.WriteLine("Pushing 1");
            subject.OnNext(1);
            Console.WriteLine("Pushing 2");
            subject.OnNext(2);
            Console.WriteLine("Pushing 3");
            subject.OnNext(3);
            Console.WriteLine("Pushing 4");
            subject.OnNext(4);
            subject.OnCompleted();
        }

        static void TakeLastMethod()
        {
            var subject = new Subject<int>();
            subject
            .TakeLast(2)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            Console.WriteLine("Pushing 1");
            subject.OnNext(1);
            Console.WriteLine("Pushing 2");
            subject.OnNext(2);
            Console.WriteLine("Pushing 3");
            subject.OnNext(3);
            Console.WriteLine("Pushing 4");
            subject.OnNext(4);
            Console.WriteLine("Completing");
            subject.OnCompleted();
        }

        static void SkipUntillMethod()
        {
            var subject = new Subject<int>();
            var otherSubject = new Subject<Unit>();
            subject
            .SkipUntil(otherSubject)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            otherSubject.OnNext(Unit.Default);
            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);
            subject.OnNext(7);
            subject.OnNext(8);
            subject.OnCompleted();
        }

        static void TakeUntillMethod()
        {
            var subject = new Subject<int>();
            var otherSubject = new Subject<Unit>();
            subject
            .TakeUntil(otherSubject)
            .Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
            subject.OnNext(1);
            subject.OnNext(2);
            subject.OnNext(3);
            otherSubject.OnNext(Unit.Default);
            subject.OnNext(4);
            subject.OnNext(5);
            subject.OnNext(6);
            subject.OnNext(7);
            subject.OnNext(8);
            subject.OnCompleted();
        }
    }
}
