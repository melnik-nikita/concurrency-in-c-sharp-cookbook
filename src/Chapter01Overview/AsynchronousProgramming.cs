namespace Chapter01Overview;

using System;
using System.Threading.Tasks;

internal class AsynchronousProgramming
{
    /*
     * It’s good practice to always call ConfigureAwait in your core “library” methods,
     * and only resume the context when you need it—in your outer “user interface”
     * methods.
     */
    public static async Task DoSomethingAsync()
    {
        int value = 13;

        // Asynchronously wait 1 second.
        await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

        value *= 2;

        // Asynchronously wait 1 second.
        await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

        Console.WriteLine(value);
    }

    public static async Task TrySomethingAsync()
    {
        // The exception will end up on the Task, not thrown directly.
        Task task = PossibleExceptionAsync();
        try
        {
            //await PossibleExceptionAsync();
            // The Task's exception will be raised here, at the await.
            await task;
        }
        catch (NotSupportedException ex)
        {
            LogException(ex);
            throw;
        }
    }

    public static async Task WaitAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
    }

    public static void Deadlock()
    {
        // Start the delay.
        Task task = WaitAsync();

        // Synchronously block, waiting for the async method to complete.
        task.Wait();
    }

    private static Task PossibleExceptionAsync()
    {
        throw new NotSupportedException();
    }

    private static void LogException(NotSupportedException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
