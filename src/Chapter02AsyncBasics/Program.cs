using Chapter02AsyncBasics;

//var result = await AsyncBasics.DelayResult(5, TimeSpan.FromSeconds(5));
//Console.WriteLine($"Result {result}");

await AsyncBasics.ObserveOneExceptionAsync();
await AsyncBasics.ObserveAllExceptionsAsync();

Console.ReadLine();
