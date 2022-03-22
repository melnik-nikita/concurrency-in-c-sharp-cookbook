namespace Chapter03AsynchronousStreams;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class AsynchronousStreams
{
    public static async IAsyncEnumerable<int> GetAsyncStream()
    {
        await Task.Delay(1000);
        yield return 10;
        await Task.Delay(1000);
        yield return 13;
        await Task.Delay(3000);
        yield return 18;
    }

    public static async Task ConsumeAsyncStream()
    {
        await foreach(var value in GetAsyncStream())
        {
            Console.WriteLine(value);
        }
    }
}
