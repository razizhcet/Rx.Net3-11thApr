using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.NetSequenceBasics
{
    class SimpleFactoryMethods
    {
        static void Main(string[] args)
        {
            //BlockingMethod();
            NonBlocking();
            Console.ReadKey();
        }

        static IObservable<string> BlockingMethod()
        {
            var subject = new ReplaySubject<string>();
            subject.OnNext("a");
            subject.OnNext("b");
            subject.OnCompleted();
            Thread.Sleep(1000);
            return subject;
        }
        static IObservable<string> NonBlocking()
        {
            return Observable.Create<string>((IObserver<string> observer) =>
            {
                observer.OnNext("a");
                observer.OnNext("b");
                observer.OnCompleted();
                Thread.Sleep(1000);
                return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed"));
                //or can return an Action like 
                //return () => Console.WriteLine("Observer has unsubscribed"); 
            });
        }
    }

}
