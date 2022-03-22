using System.Diagnostics;
using System.Reactive.Linq;
using Chapter01Overview;

#region AsynchronousProgramming

//await AsynchronousProgramming.DoSomethingAsync();

//await AsynchronousProgramming.TrySomethingAsync();

// The code in this example will deadlock if called from a UI or ASP.NET
// Classic context because both of those contexts only allow one thread in at
// a time.
//AsynchronousProgramming.Deadlock();

#endregion AsynchronousProgramming

#region ParallelProgramming

//ParallelProgramming.RotateMatrices(new List<Matrix> { new(3), new(13), new(0) }, 5);

//ParallelProgramming.PrimalityTest(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 }).ToList();

//Parallel.Invoke(
//    () => Console.WriteLine(1),
//    () => Console.WriteLine(2),
//    () => Console.WriteLine(3),
//    () => Console.WriteLine(4)
//);

//try
//{
//    Parallel.Invoke(() => { throw new Exception("First"); },
//    () => { throw new Exception("Second"); });
//}
//catch (AggregateException ex)
//{
//    ex.Handle(exception =>
//    {
//        Console.WriteLine(exception.Message);
//        return true; // "handled"
//    });
//}

#endregion ParallelProgramming

#region ReactiveProgramming

var timestamps = Observable.Interval(TimeSpan.FromSeconds(1))
    .Timestamp()
    .Where(x => x.Value % 2 == 0)
    .Select(x => x.Timestamp);

var timestampsDisposable = timestamps
    .Subscribe(
        x => Console.WriteLine(x),
        ex => Console.WriteLine(ex.Message)
    );

#endregion ReactiveProgramming
Console.ReadLine();

timestampsDisposable.Dispose();
Console.ReadLine();
Console.WriteLine("Finish");
