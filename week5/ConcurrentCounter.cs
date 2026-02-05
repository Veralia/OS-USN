//The Problem: Concurrent Counter
using System;
using System.Threading;

class RaceConditionDemo
{
    static int counter = 0;  // Shared variable

    static void Increment()
    {
        for (int i = 0; i < 1000; i++)
        {
            counter = counter + 1;
        }
    }

    static void Decrement()
    {
        for (int i = 0; i < 1000; i++)
        {
            counter = counter - 1;
        }
    }

    static void Main()
    {
        Thread thread1 = new Thread(Increment);
        Thread thread2 = new Thread(Decrement);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine($"Final counter value: {counter}");
        Console.WriteLine("Expected value: 0");
    }
}
