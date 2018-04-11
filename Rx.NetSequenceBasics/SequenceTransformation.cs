using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Rx.NetSequenceBasics
{
    public static class SequenceTransformation
    {
        //public static void Dump<T>(this IObservable<T> source, string name)
        //{
        //    source.Subscribe(
        //    i => Console.WriteLine("{0}-->{1}", name, i),
        //    ex => Console.WriteLine("{0} failed-->{1}", name, ex.Message),
        //    () => Console.WriteLine("{0} completed", name));
        //}

        static void Main(string[] args)
        {
            //SelectMethod();
            //CharTransform();
            //CastMethod();
            //OfTypeMethod();
            //TimestampMethod();
            //TimeIntervalMethod();
            //MaterializeMethod1();
            //MaterializeMethod2();
            //SelectManyMethod1();
            //SelectManyMethod2();
            //SelectManyChar1();
            //SelectManyChar2();
            SelectManyChar3();
            Console.ReadKey();
        }

        static void SelectMethod()
        {
            var source = Observable.Range(0, 5);
            source.Select(i => i + 3)
            .Dump("+3");
        }

        static void CharTransform()
        {
            //Observable.Range(1, 5)
            //.Select(
            //i => new { Number = i, Character = (char)(i + 64) })
            //.Dump("transform");

            var query = from i in Observable.Range(1, 5)
            select new { Number = i, Character = (char)(i + 64) };
            query.Dump("anon");
        }

        static void CastMethod()
        {
            var objects = new Subject<object>();
            objects.Cast<int>().Dump("cast");
            objects.OnNext(1);
            objects.OnNext(2);
            objects.OnNext(3);
            objects.OnCompleted();
        }

        static void OfTypeMethod()
        {
            var objects = new Subject<object>();
            objects.OfType<int>().Dump("OfType");
            objects.OnNext(1);
            objects.OnNext(2);
            objects.OnNext("3");//Ignored
            objects.OnNext(4);
            objects.OnCompleted();
        }

        static void TimestampMethod()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(3)
                .Timestamp()
                .Dump("TimeStamp");
        }

        static void TimeIntervalMethod()
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(3)
                .TimeInterval()
                .Dump("TimeInterval");
        }

        static void MaterializeMethod1()
        {
            Observable.Range(1, 3)
                .Materialize()
                .Dump("Materialize");
        }

        static void MaterializeMethod2()
        {
            var source = new Subject<int>();
            source.Materialize()
            .Dump("Materialize");
            source.OnNext(1);
            source.OnNext(2);
            source.OnNext(3);
            source.OnError(new Exception("Fail?"));
        }

        static void SelectManyMethod1()
        {
            Observable.Return(3)
            .SelectMany(i => Observable.Range(1, i))
            .Dump("SelectMany");
        }

        static void SelectManyMethod2()
        {
            Observable.Range(1, 3)
            .SelectMany(i => Observable.Range(1, i))
            .Dump("SelectMany");
        }

        static void SelectManyChar1()
        {
            Func<int, char> letter = i => (char)(i + 64);
            Observable.Return(3)
            .SelectMany(i => Observable.Return(letter(i)))
            .Dump("SelectMany");
        }

        static void SelectManyChar2()
        {
            Func<int, char> letter = i => (char)(i + 64);
            Observable.Range(1, 3)
            .SelectMany(i => Observable.Return(letter(i)))
            .Dump("SelectMany");
        }

        static void SelectManyChar3()
        {
            Func<int, char> letter = i => (char)(i + 64);
            Observable.Range(1, 30)
            .SelectMany(
            i =>
            {
                if (0 < i && i < 27)
                {
                    return Observable.Return(letter(i));
                }
                else
                {
                    return Observable.Empty<char>();
                }
            })
            .Dump("SelectMany");
        }
    }
}
