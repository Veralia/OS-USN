# Process Management - Coding Assignment

**Estimated Time:** 45 minutes  
**Prerequisites:** Complete the Process Management Tutorial

## 📋 Assignment Overview

You will build a **System Process Manager** - a console application that helps users manage and monitor processes on their system. This assignment tests your understanding of process creation, monitoring, I/O redirection, and process control.

---

## 🎯 Problem 1: Process Information Viewer (15 minutes)

### Description
Create a program that displays detailed information about a specific process.

### Requirements

Create a method `DisplayProcessInfo(string processName)` that:

1. Searches for all processes with the given name
2. For each process found, display:
   - Process ID
   - Process Name
   - Start Time (handle exceptions if not accessible)
   - Memory Usage (Working Set in MB)
   - Priority Class
   - Responding status (true/false)

3. If no process is found, display an appropriate message

### Example Output
```
=== Process Information for "chrome" ===

Process #1:
  ID: 12345
  Name: chrome
  Start Time: 1/20/2025 10:30:45 AM
  Memory Usage: 145.5 MB
  Priority: Normal
  Responding: Yes

Process #2:
  ID: 12346
  Name: chrome
  Start Time: 1/20/2025 10:31:12 AM
  Memory Usage: 98.3 MB
  Priority: Normal
  Responding: Yes

Total processes found: 2
```

### Hints
- Use `Process.GetProcessesByName(processName)`
- Memory: `process.WorkingSet64 / (1024 * 1024)` converts bytes to MB
- Some properties may throw exceptions - use try-catch blocks
- Use `process.Responding` to check if UI is responding

### Starter Code
```csharp
static void DisplayProcessInfo(string processName)
{
    // TODO: Implement this method
}
```

---

## 🎯 Problem 2: Command Executor with Logging (15 minutes)

### Description
Create a utility that executes command-line commands and logs both their output and errors to a file.

### Requirements

Create a method `ExecuteAndLog(string command, string arguments, string logFilePath)` that:

1. Executes the given command with arguments silently (no window)
2. Captures both standard output and standard error
3. Writes to the log file:
   - Timestamp of execution
   - Command executed
   - Arguments used
   - Exit code
   - Standard output (if any)
   - Standard error (if any)
   - Separator line

4. Returns `true` if exit code is 0, `false` otherwise


### Example Log File Content
```
================================================================================
Execution Time: 1/20/2025 2:45:30 PM
Command: cmd.exe
Arguments: /c dir C:\Windows
Exit Code: 0

Standard Output:
 Volume in drive C is Windows
 Volume Serial Number is XXXX-XXXX

 Directory of C:\Windows

[... directory listing ...]

Standard Error:
[None]
================================================================================
```

### Hints
- Set both `RedirectStandardOutput` and `RedirectStandardError` to `true`
- Use `File.AppendAllText()` or `StreamWriter` for logging
- Read output and error streams before `WaitForExit()`
- Use `DateTime.Now` for timestamps

### Starter Code
```csharp
static bool ExecuteAndLog(string command, string arguments, string logFilePath)
{
    // TODO: Implement this method
    return false;
}
```

---

## 🎯 Problem 3: Process Watchdog (15 minutes)

### Description
Create a process monitor that ensures a critical application stays running. If it crashes or is closed, the watchdog automatically restarts it.

### Requirements

Create a method `WatchProcess(string executablePath, int checkIntervalSeconds, int maxRestarts)` that:

1. Starts the specified executable
2. Checks every `checkIntervalSeconds` if the process is still running
3. If the process has exited:
   - Display a message indicating the process stopped
   - Display the exit code
   - Restart the process (if under `maxRestarts` limit)
   - Increment restart counter
4. If `maxRestarts` is reached, stop monitoring and display final message
5. Allow user to press 'Q' to quit the watchdog gracefully
6. When quitting, ask user if they want to close the monitored process

### Example Output
```
=== Process Watchdog Started ===
Monitoring: C:\Windows\notepad.exe
Check Interval: 5 seconds
Max Restarts: 3

Process started with PID: 12345
Press 'Q' to quit the watchdog...

Checking process... [OK]
Checking process... [OK]

WARNING: Process has exited!
Exit Code: 0
Restart Count: 1/3
Restarting process...
Process restarted with PID: 12456

Checking process... [OK]

User pressed 'Q' - Stopping watchdog...
Do you want to close the monitored process? (Y/N): Y
Process closed. Watchdog stopped.
```

### Hints
- Use `Process.HasExited` to check if process is still running
- Use `Thread.Sleep(checkIntervalSeconds * 1000)` for delays
- Use `Console.KeyAvailable` and `Console.ReadKey(true)` for non-blocking input
- Store the process object in a variable you can check repeatedly
- Remember to handle exceptions when the process is no longer available

### Starter Code
```csharp
static void WatchProcess(string executablePath, int checkIntervalSeconds, int maxRestarts)
{
    // TODO: Implement this method
}
```

---

## 🎯 Extra: System Resource Monitor

### Description
Create a real-time system monitor that displays CPU and memory usage of the top 5 most resource-intensive processes.

### Requirements

Create a method `MonitorSystemResources(int refreshSeconds, int duration)` that:

1. Runs for `duration` seconds, refreshing every `refreshSeconds`
2. Each refresh displays:
   - Total number of running processes
   - Top 5 processes by CPU usage (percentage)
   - Top 5 processes by memory usage (MB)
   - Current timestamp

3. Clears the console between refreshes for a clean display
4. Allows early exit by pressing 'X'

### Example Output
```
=== System Resource Monitor ===
Refresh: 1/20/2025 2:50:15 PM
Total Processes: 247

Top 5 by CPU Usage:
  1. chrome.exe        - 15.3%  (PID: 12345)
  2. vsCode.exe        - 8.7%   (PID: 23456)
  3. firefox.exe       - 5.2%   (PID: 34567)
  4. steam.exe         - 3.1%   (PID: 45678)
  5. Discord.exe       - 2.4%   (PID: 56789)

Top 5 by Memory Usage:
  1. chrome.exe        - 1024.5 MB  (PID: 12345)
  2. firefox.exe       - 856.3 MB   (PID: 34567)
  3. vsCode.exe        - 512.7 MB   (PID: 23456)
  4. steam.exe         - 345.2 MB   (PID: 45678)
  5. Teams.exe         - 287.6 MB   (PID: 67890)

Press 'X' to exit...
```

### Hints
- Use `process.TotalProcessorTime` to calculate CPU usage
- CPU calculation: Compare TotalProcessorTime between intervals
- Use LINQ `.OrderByDescending()` to sort processes
- `Console.Clear()` clears the screen
- You'll need to track previous CPU times to calculate percentage

---
### Testing Your Code

Test each problem with:

**Problem 1:**
- Valid process name (e.g., "notepad")
- Invalid process name (e.g., "nonexistent")
- System process (may have permission issues)

**Problem 2:**
- Valid command: `cmd.exe /c dir`
- Command with error: `cmd.exe /c dir Z:\FakeFolder`
- Multiple executions (verify log appending)

**Problem 3:**
- Start Notepad, close it manually, verify restart
- Reach max restart limit
- Press 'Q' to exit gracefully

---


## 💡 Tips for Success

1. **Read error messages** - They often tell you exactly what's wrong
2. **Use the tutorial** - Reference the examples for similar patterns
3. **Don't hardcode paths** - Use methods that work on any Windows system

---

**Good luck!** 
