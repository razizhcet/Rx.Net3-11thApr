using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rx.NetSequenceBasics
{
    class RxSelectMany
    {
        static void Main(string[] args)
        {
            //EnumerableSelectMany();
            //ObservableSelectMany();
            SelectManyOperator();
            Console.ReadKey();
        }

        /*
        static void EnumerableSelectMany()
        {
            var enumerableSource = new[] { 1, 2, 3 };
            var enumerableResult = enumerableSource.SelectMany(GetSubValues);
            foreach (var value in enumerableResult)
            {
                Console.WriteLine(value);
            }
        }

        static IEnumerable<int> GetSubValues(int offset)
        {
            yield return offset * 10;
            yield return (offset * 10) + 1;
            yield return (offset * 10) + 2;
        }
        */

        static void ObservableSelectMany()
        {
            // Values [1,2,3] 3 seconds apart.
            Observable.Interval(TimeSpan.FromSeconds(3))
            .Select(i => i + 1) //Values start at 0, so add 1.
            .Take(3)            //We only want 3 values
            .SelectMany(GetSubValues) //project into child sequences
            .Dump("SelectMany");
        }

        static IObservable<long> GetSubValues(long offset)
        {
            //Produce values [x*10, (x*10)+1, (x*10)+2] 4 seconds apart, but starting immediately.
            return Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(4))
            .Select(x => (offset * 10) + x)
            .Take(3);
        }

        static void SelectManyOperator()
        {
            var query = from i in Observable.Range(1, 5)
                        where i % 2 == 0
                        from j in GetSubValues(i)
                        select new { i, j };
            query.Dump("SelectMany");
        }
    }
}
