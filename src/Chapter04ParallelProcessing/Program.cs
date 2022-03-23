using Chapter04ParallelProcessing;
using Common;

IList<Matrix> matrices = new[]
{
    new Matrix("1", true),
    new Matrix("2", true),
    new Matrix("3", true),
    new Matrix("4", true),
    new Matrix("5", true),
    new Matrix("6", false),
    new Matrix("7", true),
    new Matrix("8", true),
    new Matrix("9", false),
    new Matrix("10", true)
};

//ParallelProcessing.RotateMatrices(matrices, 10);
//ParallelProcessing.InvertMatrices(matrices);
var nonInvertedCount = ParallelProcessing.InvertMatricesWithNonRevertedResult(matrices);
//Console.WriteLine($"Number of matrices not inverted: {nonInvertedCount}");

IList<int> values = new [] { 3, 45, 6, 57, 57, 5, 8, 8 };

Console.WriteLine($"Sum is: {ParallelProcessing.ParallelSumV1(values)}");
Console.WriteLine($"Sum is: {ParallelProcessing.ParallelSumV2(values)}");
Console.WriteLine($"Sum is: {ParallelProcessing.ParallelSumV3(values)}");

Console.ReadLine();
