using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ProcessManagementAssignment
{
    /// <summary>
    /// Process Management Assignment - Starter Template
    /// Student Name: ____________________
    /// Date: ____________________
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Process Management Assignment ===\n");
            
            // TODO: Uncomment the problem you want to test
            
            // TestProblem1();
            // TestProblem2();
            // TestProblem3();
            // TestBonusProblem();
        }

        #region Test Methods
        
        /// <summary>
        /// Test method for Problem 1 - Process Information Viewer
        /// </summary>
        static void TestProblem1()
        {
            Console.WriteLine("=== Testing Problem 1: Process Information Viewer ===\n");
            
            // Test with a common process
            Console.WriteLine("Test 1: Valid process (notepad)");
            Console.WriteLine("Please start Notepad before testing...");
            Console.WriteLine("Press any key when ready...");
            Console.ReadKey();
            DisplayProcessInfo("notepad");
            
            Console.WriteLine("\n" + new string('-', 60) + "\n");
            
            // Test with non-existent process
            Console.WriteLine("Test 2: Non-existent process");
            DisplayProcessInfo("ThisProcessDoesNotExist");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Test method for Problem 2 - Command Executor with Logging
        /// </summary>
        static void TestProblem2()
        {
            Console.WriteLine("=== Testing Problem 2: Command Executor with Logging ===\n");
            
            string logFile = "command_log.txt";
            
            // Test 1: Successful command
            Console.WriteLine("Test 1: Executing 'dir' command...");
            bool success1 = ExecuteAndLog("cmd.exe", "/c dir", logFile);
            Console.WriteLine($"Result: {(success1 ? "Success" : "Failed")}");
            
            Console.WriteLine("\nTest 2: Executing command with error...");
            bool success2 = ExecuteAndLog("cmd.exe", "/c dir Z:\\NonExistentFolder", logFile);
            Console.WriteLine($"Result: {(success2 ? "Success" : "Failed")}");
            
            Console.WriteLine("\nTest 3: Another successful command...");
            bool success3 = ExecuteAndLog("cmd.exe", "/c echo Hello from Command Executor!", logFile);
            Console.WriteLine($"Result: {(success3 ? "Success" : "Failed")}");
            
            Console.WriteLine($"\nCheck '{logFile}' to see the logged output.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Test method for Problem 3 - Process Watchdog
        /// </summary>
        static void TestProblem3()
        {
            Console.WriteLine("=== Testing Problem 3: Process Watchdog ===\n");
            
            Console.WriteLine("This will start Notepad and monitor it.");
            Console.WriteLine("Try closing Notepad to see it restart.");
            Console.WriteLine("Press 'Q' to stop the watchdog.\n");
            
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            
            WatchProcess(@"C:\Windows\notepad.exe", 5, 3);
            
            Console.WriteLine("\nWatchdog test complete.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        /// <summary>
        /// Test method for Bonus Problem - System Resource Monitor
        /// </summary>
        static void TestBonusProblem()
        {
            Console.WriteLine("=== Testing Bonus Problem: System Resource Monitor ===\n");
            
            Console.WriteLine("This will monitor system resources for 30 seconds.");
            Console.WriteLine("Press 'X' to exit early.\n");
            
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            
            MonitorSystemResources(3, 30);
            
            Console.WriteLine("\nMonitoring complete.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        #endregion

        #region Problem 1: Process Information Viewer

        /// <summary>
        /// Problem 1: Display detailed information about all processes with the given name
        /// </summary>
        /// <param name="processName">The name of the process to search for</param>
        static void DisplayProcessInfo(string processName)
        {
            Console.WriteLine($"=== Process Information for \"{processName}\" ===\n");
            
            // TODO: Implement this method
            // Requirements:
            // 1. Use Process.GetProcessesByName() to find all matching processes
            // 2. Display the following for each process:
            //    - Process ID
            //    - Process Name
            //    - Start Time (handle exceptions)
            //    - Memory Usage in MB (WorkingSet64 / 1024 / 1024)
            //    - Priority Class
            //    - Responding status
            // 4. Display total count of processes found
            // 5. Display appropriate message if no processes found
            
            Console.WriteLine("TODO: Implement Process Information Viewer");
        }

        #endregion

        #region Problem 2: Command Executor with Logging

        /// <summary>
        /// Problem 2: Execute a command and log its output and errors to a file
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <param name="arguments">Arguments for the command</param>
        /// <param name="logFilePath">Path to the log file</param>
        /// <returns>True if exit code is 0, false otherwise</returns>
        static bool ExecuteAndLog(string command, string arguments, string logFilePath)
        {
            // TODO: Implement this method
            // Requirements:
            // 1. Create ProcessStartInfo with:
            //    - RedirectStandardOutput = true
            //    - RedirectStandardError = true
            //    - UseShellExecute = false
            //    - CreateNoWindow = true
            // 2. Execute the process and capture both output and error streams
            // 3. Write to log file with:
            //    - Separator line (80 equals signs)
            //    - Execution timestamp
            //    - Command and arguments
            //    - Exit code
            //    - Standard output
            //    - Standard error
            //    - Another separator line
            // 4. Use File.AppendAllText() or StreamWriter to append to log
            // 5. Return true if exit code is 0, false otherwise
            
            Console.WriteLine("TODO: Implement Command Executor with Logging");
            return false;
        }

        #endregion

        #region Problem 3: Process Watchdog

        /// <summary>
        /// Problem 3: Monitor a process and restart it if it exits
        /// </summary>
        /// <param name="executablePath">Full path to the executable to monitor</param>
        /// <param name="checkIntervalSeconds">How often to check if process is running</param>
        /// <param name="maxRestarts">Maximum number of times to restart the process</param>
        static void WatchProcess(string executablePath, int checkIntervalSeconds, int maxRestarts)
        {
            // TODO: Implement this method
            // Requirements:
            // 1. Start the process and store it in a variable
            // 2. Display initial information (path, interval, max restarts, PID)
            // 3. In a loop:
            //    - Check if user pressed 'Q' (use Console.KeyAvailable and Console.ReadKey)
            //    - Check if process has exited (use process.HasExited)
            //    - If exited and under max restarts:
            //      * Display exit code
            //      * Increment restart counter
            //      * Restart the process
            //    - If max restarts reached, exit loop
            //    - Sleep for checkIntervalSeconds
            // 4. When exiting (either by 'Q' or max restarts):
            //    - Ask user if they want to close the process
            //    - Close process if requested and still running

            
            Console.WriteLine("TODO: Implement Process Watchdog");
        }

        #endregion

        #region Bonus Problem: System Resource Monitor

        /// <summary>
        /// Bonus Problem: Display real-time system resource usage
        /// </summary>
        /// <param name="refreshSeconds">How often to refresh the display</param>
        /// <param name="duration">Total duration to run in seconds</param>
        static void MonitorSystemResources(int refreshSeconds, int duration)
        {
            // TODO: Implement this method (BONUS - Optional)
            // Requirements:
            // 1. Run for 'duration' seconds total
            // 2. Refresh display every 'refreshSeconds'
            // 3. Each refresh:
            //    - Clear the console
            //    - Display timestamp
            //    - Display total process count
            //    - Display top 5 processes by CPU usage
            //    - Display top 5 processes by Memory usage
            // 4. Allow early exit by pressing 'X'
            // 5. Use LINQ to sort and take top 5
            // 
            // Hints for CPU calculation:
            // - You need to sample TotalProcessorTime twice with a delay
            // - CPU% = (Time2 - Time1) / (Elapsed * ProcessorCount) * 100
            
            Console.WriteLine("TODO: Implement System Resource Monitor (BONUS)");
        }

        #endregion


    }
}
