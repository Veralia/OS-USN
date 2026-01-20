using System;
using System.Diagnostics;
using System.IO;

namespace ProcessManagementTutorial
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Process Management Tutorial in C# ===\n");
            Console.WriteLine("Uncomment the example you want to run in the Main method.\n");

            // Uncomment one of the following examples to test:
            
            // Example1_StartSimpleProcess();
            // Example2_StartProcessWithArguments();
            // Example3_MonitorRunningProcesses();
            // Example4_KillProcessByName();
            // Example5_StartProcessWithStartInfo();
            // Example6_ControlProcessPriority();
            // Example7_RunProcessSilently();
            // Example8_UseEnvironmentVariables();
            // Example9_RedirectStandardInput();
            // Example10_RedirectStandardOutput();
        }

        /// <summary>
        /// Example 1: Starting a Simple Process
        /// Launches Notepad without any additional configuration.
        /// </summary>
        static void Example1_StartSimpleProcess()
        {
           Process.Start("notepad.exe");
           
        }

        /// <summary>
        /// Example 2: Starting a Process with Arguments
        /// Launches Notepad and opens a specific file.
        /// </summary>
        static void Example2_StartProcessWithArguments()
        {
                // Opens Notepad with the Windows INI file
                Process.Start(@"C:\Windows\notepad.exe", @"C:\Windows\win.ini");
                
        }

        /// <summary>
        /// Example 3: Monitoring Running Processes
        /// Lists all currently running processes with their IDs.
        /// </summary>
        static void Example3_MonitorRunningProcesses()
        {
              Process[] processes = Process.GetProcesses();

                Console.WriteLine($"Total running processes: {processes.Length}\n");
                Console.WriteLine("First 20 processes:");
                
                foreach (Process process in processes)
                {
                    System.Console.WriteLine($"Process: {process.ProcessName}, ID: {process.Id}");
                }
        }

        /// <summary>
        /// Example 4: Killing a Process by Name
        /// Terminates all Notepad processes (use with caution!).
        /// </summary>
        static void Example4_KillProcessByName()
        {
                Process[] notepadProcesses = Process.GetProcessesByName("notepad");

        
                foreach (Process process in notepadProcesses)
                {
                    process.Kill();
                    Console.WriteLine($"Killed process: {process.ProcessName}, ID: {process.Id}");
                }
            

        }

        /// <summary>
        /// Example 5: Starting Process with ProcessStartInfo
        /// Provides more control over how the process is started.
        /// </summary>
        static void Example5_StartProcessWithStartInfo()
        {
          
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Windows\notepad.exe",
                    Arguments = @"C:\Windows\win.ini"
                };

                Process.Start(startInfo);
                Console.WriteLine("Process started using ProcessStartInfo!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Example 6: Controlling Process Priority
        /// Sets the CPU priority of a started process.
        /// Priority Levels: Idle, BelowNormal, Normal (default), AboveNormal, High, RealTime
        /// </summary>
        static void Example6_ControlProcessPriority()
        {
            Console.WriteLine("--- Example 6: Controlling Process Priority ---");
            
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "notepad.exe"
                };

                var process = new Process
                {
                    StartInfo = startInfo
                };

                process.Start();

                // Set process priority to BelowNormal
                process.PriorityClass = ProcessPriorityClass.BelowNormal;

                Console.WriteLine($"Process ID: {process.Id}");
                Console.WriteLine($"Priority: {process.PriorityClass}");
                Console.WriteLine("Notepad is now running with BelowNormal priority.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Example 7: Running Processes Silently
        /// Executes a command without showing a window.
        /// </summary>
        static void Example7_RunProcessSilently()
        {
            Console.WriteLine("--- Example 7: Running Process Silently ---");
            
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c dir",
                    CreateNoWindow = true,      // Don't show the console window
                    UseShellExecute = false     // Required for CreateNoWindow
                };

                var process = new Process
                {
                    StartInfo = startInfo
                };

                process.Start();
                process.WaitForExit();

                Console.WriteLine("Silent process completed!");
                Console.WriteLine($"Exit code: {process.ExitCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Example 8: Working with Environment Variables
        /// Sets custom environment variables for the child process.
        /// </summary>
        static void Example8_UseEnvironmentVariables()
        {
            Console.WriteLine("--- Example 8: Working with Environment Variables ---");
            
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c echo %MY_VAR%",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Add custom environment variable
                startInfo.EnvironmentVariables["MY_VAR"] = "Hello, Environment!";

                Process process = new Process
                {
                    StartInfo = startInfo
                };

                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine($"Output: {output.Trim()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Example 9: Redirecting Standard Input
        /// Sends commands to a process via its standard input stream.
        /// </summary>
        static void Example9_RedirectStandardInput()
        {
            Console.WriteLine("--- Example 9: Redirecting Standard Input ---");
            
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process process = new Process
                {
                    StartInfo = startInfo
                };

                process.Start();

                // Write commands to the process's input
                using (StreamWriter writer = process.StandardInput)
                {
                    if (writer.BaseStream.CanWrite)
                    {
                        writer.WriteLine("echo Hello from ProcessStartInfo!");
                        writer.WriteLine("echo Current directory:");
                        writer.WriteLine("cd");
                        writer.WriteLine("exit");
                    }
                }

                // Read the output
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine("Output from cmd.exe:");
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Example 10: Redirecting Standard Output
        /// Captures and displays the output of a process.
        /// </summary>
        static void Example10_RedirectStandardOutput()
        {
            Console.WriteLine("--- Example 10: Redirecting Standard Output ---");
            
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c dir",       // Run 'dir' command
                    RedirectStandardOutput = true,  // Capture the output
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();

                    // Read and display the output
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    
                    Console.WriteLine("Directory listing:");
                    Console.WriteLine(output);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
