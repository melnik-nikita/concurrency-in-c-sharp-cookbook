using System.Threading.Tasks.Dataflow;

var multiplyBlock = new TransformBlock<int, int>(item => item * 2);
var subtracBlock = new TransformBlock<int, int>(item => item - 2);
// After linking,values that exit multiplyBlock will enter subtractBlock.

multiplyBlock.LinkTo(subtracBlock);

var result = multiplyBlock.Post(10);

Console.WriteLine($"Result: {result}");

Console.ReadLine();
