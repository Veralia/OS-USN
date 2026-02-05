using System;
using System.Threading;

class FixedWithLock
{
    static int counter = 0;
    static readonly object lockObject = new object();

    static void Increment()
    {
        for (int i = 0; i < 100000; i++)
        {
            lock (lockObject)
            {
                counter++;
            }
        }
    }

    static void Decrement()
    {
        for (int i = 0; i < 100000; i++)
        {
            lock (lockObject)
            {
                counter--;
            }
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
