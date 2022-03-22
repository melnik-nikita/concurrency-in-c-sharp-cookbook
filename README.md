# Concurrency in C# Cookbook

## Definitions
- **Concurrency** - doing more than one thing at a time.
- **Multithreading** - a form of concurrency that uses multiple threads of execution *As soon as you type new Thread(), it’s over; your project already has legacy code.*.
- **Parallel processing** - doing lots of work by dividing it up among multiple threads that run concurrently.
- **Asynchronous programming** - a form of concurrency that uses futures or callbacks to avoid unnecessary threads.
- **Reactive programming** - a declarative style of programming where the application reacts to events
- **Hot observable** - is a stream of events that is always going on, and if there are no subscribers when the events come in, they are lost.
- **Cold observable** - is an observable that doesn't have incoming events all the time. A cold observable will react to a subscription by starting the sequence of events.
