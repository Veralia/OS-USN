using System;
using System.Threading;

namespace DeadlockDemo
{
    class Program
    {
        // Two lock objects that will cause deadlock
        private static readonly object lock1 = new object();
        private static readonly object lock2 = new object();

        // Thread 1: Acquires lock1 first, then lock2
        static void Thread1Method()
        {
            Console.WriteLine("Thread 1: Attempting to acquire lock1...");
            lock (lock1)
            {
                Console.WriteLine("Thread 1: Acquired lock1");
                
                // Small delay to ensure thread 2 gets lock2
                Thread.Sleep(1000);
                
                Console.WriteLine("Thread 1: Attempting to acquire lock2...");
                lock (lock2)  // This will block, waiting for lock2
                {
                    Console.WriteLine("Thread 1: Acquired lock2");
                    Console.WriteLine("Thread 1: In critical section with both locks");
                }
                Console.WriteLine("Thread 1: Released lock2");
            }
            Console.WriteLine("Thread 1: Released lock1");
        }

        // Thread 2: Acquires lock2 first, then lock1 (OPPOSITE ORDER - causes deadlock)
        static void Thread2Method()
        {
            Console.WriteLine("Thread 2: Attempting to acquire lock2...");
            lock (lock2)
            {
                Console.WriteLine("Thread 2: Acquired lock2");
                
                // Small delay to ensure thread 1 gets lock1
                Thread.Sleep(1000);
                
                Console.WriteLine("Thread 2: Attempting to acquire lock1...");
                lock (lock1)  // This will block, waiting for lock1
                {
                    Console.WriteLine("Thread 2: Acquired lock1");
                    Console.WriteLine("Thread 2: In critical section with both locks");
                }
                Console.WriteLine("Thread 2: Released lock1");
            }
            Console.WriteLine("Thread 2: Released lock2");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== Deadlock Demonstration in C# ===");
            Console.WriteLine("Creating two threads that will deadlock...\n");

            // Create both threads
            Thread thread1 = new Thread(Thread1Method);
            Thread thread2 = new Thread(Thread2Method);

            // Start both threads
            thread1.Start();
            thread2.Start();

            // Wait for threads (this will hang due to deadlock)
            thread1.Join();
            thread2.Join();

            Console.WriteLine("Program completed successfully (this won't print due to deadlock)");
        }
    }
}

/*
 * EXPLANATION:
 * 
 * This program demonstrates a classic deadlock scenario in C#:
 * 
 * 1. Thread 1 acquires lock1, then tries to acquire lock2
 * 2. Thread 2 acquires lock2, then tries to acquire lock1
 * 
 * DEADLOCK OCCURS:
 * - Thread 1 holds lock1 and waits for lock2
 * - Thread 2 holds lock2 and waits for lock1
 * - Neither can proceed (circular wait condition)
 * 
 * All four conditions for deadlock are met:
 * 1. Mutual exclusion - locks can only be held by one thread
 * 2. Hold and wait - threads hold one lock while waiting for another
 * 3. No preemption - locks cannot be forcibly taken away
 * 4. Circular wait - Thread 1 waits for Thread 2, Thread 2 waits for Thread 1
 * 
