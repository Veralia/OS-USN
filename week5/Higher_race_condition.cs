//Race condition counter with Higher Iteration Count
using System;
using System.Threading;

class RaceConditionDemo
{
    static int counter = 0;

    static void Increment()
    {
        for (int i = 0; i < 100000; i++)
        {
            counter++;
        }
    }

    static void Decrement()
    {
        for (int i = 0; i < 100000; i++)
        {
            counter--;
        }
    }

    static void Main()
    {
        Console.WriteLine("Running race condition demo 5 times:\n");

        for (int run = 1; run <= 5; run++)
        {
            counter = 0;  // Reset counter

            Thread thread1 = new Thread(Increment);
            Thread thread2 = new Thread(Decrement);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine($"Run {run}: Final value = {counter}");
        }

        Console.WriteLine("\nExpected value: 0");
        Console.WriteLine("Notice how the results vary each run!");
    }
}
