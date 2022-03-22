namespace Chapter01Overview;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class ParallelProgramming
{
    public static void RotateMatrices(IEnumerable<Matrix> matrices, float degrees)
    {
        Parallel.ForEach(matrices, matrix => matrix.Rotate(degrees));
    }

    public static IEnumerable<bool> PrimalityTest(IEnumerable<int> values)
    {
        return values.AsParallel().Select(value => IsPrime(value));
    }

    private static bool IsPrime(int value)
    {
        for (int i = 2; i < value / 2; i++)
        {
            if (value % i == 0)
            {
                Console.WriteLine($"{value} is not prime");
                return false;
            }
        }

        Console.WriteLine($"{value} is prime");
        return true;
    }
}

internal class Matrix
{
    private float _currentRotationDegree;

    public Matrix(float defaultPosition)
    {
        _currentRotationDegree = defaultPosition;
    }

    public void Rotate(float degrees)
    {
        _currentRotationDegree += degrees;
        Console.WriteLine($"Rotating on: {degrees} degrees");
        Console.WriteLine($"Currently rotated to: {_currentRotationDegree} degrees");
    }
}
