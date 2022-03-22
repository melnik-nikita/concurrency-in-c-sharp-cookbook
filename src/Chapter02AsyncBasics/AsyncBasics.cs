namespace Chapter02AsyncBasics;

internal class AsyncBasics
{
    public static async Task<T> DelayResult<T>(T result, TimeSpan delay)
    {
        await Task.Delay(delay);

        return result;
    }

    public static async Task<string> DownloadStringWithRetries(HttpClient client, string uri)
    {
        TimeSpan nextDelay = TimeSpan.FromSeconds(1);

        for (int i = 0; i != 3; i++)
        {
            try
            {
                return await client.GetStringAsync(uri);
            }
            catch
            {
            }

            await Task.Delay(nextDelay);
            nextDelay = nextDelay + nextDelay;
        }

        return await client.GetStringAsync(uri);
    }

    public static async Task<string> DwonloadStringWithTimeout(HttpClient client, string uri)
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        Task<string> downloadTask = client.GetStringAsync(uri);
        Task timeoutTask = Task.Delay(Timeout.InfiniteTimeSpan, cts.Token);

        Task completedTask = await Task.WhenAny(downloadTask, timeoutTask);

        if (completedTask == timeoutTask)
        {
            return null;
        }

        return await downloadTask;
    }

    // Reporting progress
    public static async Task MethodWithProgress(IProgress<double> progress = null)
    {
        bool done = false;
        double percentComplete = 0;
        while (!done)
        {
            progress?.Report(percentComplete);
        }
    }

    public static async Task CallMethodWithProgressAsync()
    {
        var progress = new Progress<double>();
        progress.ProgressChanged += (sender, args) =>
        {
            Console.WriteLine($"{args}% completed");
        };

        await MethodWithProgress(progress);
    }

    public async Task WaitForASetOfTasksToComplete()
    {
        Task task1 = Task.Delay(TimeSpan.FromSeconds(1));
        Task task2 = Task.Delay(TimeSpan.FromSeconds(2));
        Task task3 = Task.Delay(TimeSpan.FromSeconds(3));

        await Task.WhenAll(task1, task2, task3);
    }

    // Returns the length of data at the first URL to respond.
    public static async Task<int> FirstRespondingUrlAsync(HttpClient client, string urlA, string urlB)
    {
        // Start both downloads concurrently.
        Task<byte[]> downloadTaskA = client.GetByteArrayAsync(urlA);
        Task<byte[]> downloadTaskB = client.GetByteArrayAsync(urlB);
        // Wait for either of the tasks to complete.
        Task<byte[]> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);
        // Return the length of the data retrieved from that URL.
        byte[] data = await completedTask;

        return data.Length;
    }

    public static async Task ObserveOneExceptionAsync()
    {
        var task1 = ThrowNotImplementedExceptionAsync();
        var task2 = ThrowInvalidOperationExceptionAsync();
        try
        {
            await Task.WhenAll(task1, task2);
        }
        catch (Exception ex)
        {
            // "ex" is either NotImplementedException or
            // InvalidOperationException.
            //...
        }
    }

    public static async Task ObserveAllExceptionsAsync()
    {
        var task1 = ThrowNotImplementedExceptionAsync();
        var task2 = ThrowInvalidOperationExceptionAsync();
        Task allTasks = Task.WhenAll(task1, task2);
        try
        {
            await allTasks;
        }
        catch
        {
            AggregateException allExceptions = allTasks.Exception;
            //...
        }
    }

    static async Task ThrowNotImplementedExceptionAsync()
    {
        throw new NotImplementedException();
    }

    static async Task ThrowInvalidOperationExceptionAsync()
    {
        throw new InvalidOperationException();
    }
}
