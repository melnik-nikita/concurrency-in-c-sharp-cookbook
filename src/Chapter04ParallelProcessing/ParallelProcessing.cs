namespace Chapter04ParallelProcessing;

using System.Collections.Generic;
using Common;

internal class ParallelProcessing
{
    public static void RotateMatrices(IEnumerable<Matrix> matrices, float degrees)
    {
        Parallel.ForEach(matrices, matrix => matrix.Rotate(degrees));
    }

    public static void RotateMatrices(IEnumerable<Matrix> matrices, float degrees, CancellationToken cancellationToken)
    {
        Parallel.ForEach(matrices, new ParallelOptions { CancellationToken = cancellationToken }, matrix => matrix.Rotate(degrees));
    }

    public static void InvertMatrices(IEnumerable<Matrix> matrices)
    {
        Parallel.ForEach(matrices, (matrix, state) =>
        {
            if (!matrix.IsInvertible)
            {
                Console.WriteLine($"Matrix '{matrix.Name}' is not invertible. Stopping processing");
                state.Stop();
            }
            else
            {
                matrix.Invert();
            }
        });
    }

    public static int InvertMatricesWithNonRevertedResult(IEnumerable<Matrix> matrices)
    {
        object mutex = new object();
        int nonInvertibleMatricesCount = 0;

        Parallel.ForEach(matrices, matrix =>
        {
            if (matrix.IsInvertible)
            {
                matrix.Invert();
            }
            else
            {
                lock (mutex)
                {
                    nonInvertibleMatricesCount++;
                }
            }
        });

        return nonInvertibleMatricesCount;
    }

    // Note: this is not the most efficient implementation.
    // This is just an example of using a lock to protect shared state.
    public static int ParallelSumV1(IEnumerable<int> values)
    {
        object mutex = new();
        int result = 0;
        Parallel.ForEach(
            source: values,
            localInit: () => 0,
            body: (item, state, localValue) => localValue + item,
            localFinally: localValue =>
            {
                lock(mutex)
                {
                    result += localValue;
                }
            }
        );

        return result;
    }

    public static int ParallelSumV2(IEnumerable<int> values)
    {
        return values.AsParallel().Sum();
    }

    public static int ParallelSumV3(IEnumerable<int> values)
    {
        return values.AsParallel().Aggregate(
            seed: 10,
            func: (sum, item) =>
            {
                return sum + item;
            }
        );
    }
}
