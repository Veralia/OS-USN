# Process Management Tutorial in C#

A comprehensive guide to launching and controlling processes in .NET applications using the `System.Diagnostics.Process` class.

## 📚 Learning Objectives

By completing this tutorial, you will learn how to:
- Start and stop processes programmatically
- Monitor running processes
- Control process priority and behavior
- Redirect input/output streams
- Work with environment variables
- Run processes silently in the background

## 🚀 Getting Started

### Prerequisites
- .NET 6.0 or later
- Visual Studio, Visual Studio Code, or any C# IDE (I use Rider)
- Windows OS (examples use Windows-specific executables)

### Running the Examples

1. Open the `ProcessManagementTutorial.cs` file
2. In the `Main()` method, uncomment the example you want to run
3. Build and run the program

```csharp
static void Main()
{
    // Uncomment one example at a time:
    Example1_StartSimpleProcess();
}
```

## 📖 Tutorial Contents

### Example 1: Starting a Simple Process
**Concept:** Basic process launching  
**Use Case:** Open external applications from your program

```csharp
Process.Start("notepad.exe");
```

**Key Points:**
- Simplest way to launch an application
- Limited control over the process
- Returns immediately (non-blocking)

---

### Example 2: Starting a Process with Arguments
**Concept:** Passing command-line arguments  
**Use Case:** Open files in specific applications

```csharp
Process.Start(@"C:\Windows\notepad.exe", @"C:\Windows\win.ini");
```

**Key Points:**
- Second parameter contains arguments
- Use full paths for reliability
- Arguments are space-separated strings

---

### Example 3: Monitoring Running Processes
**Concept:** System process enumeration  
**Use Case:** Task manager functionality, system monitoring

```csharp
Process[] processes = Process.GetProcesses();
foreach (Process process in processes)
{
    Console.WriteLine($"Process: {process.ProcessName}, ID: {process.Id}");
}
```

**Key Points:**
- `GetProcesses()` returns all running processes
- Each process has unique PID (Process ID)
- Useful for diagnostics and monitoring

---

### Example 4: Killing a Process by Name
**Concept:** Process termination  
**Use Case:** Cleanup operations, force-closing applications

```csharp
Process[] notepadProcesses = Process.GetProcessesByName("notepad");
foreach (Process process in notepadProcesses)
{
    process.Kill();
}
```

**⚠️ Warning:**
- Use `Kill()` with caution - forces immediate termination
- No chance for the process to save data
- Consider using `CloseMainWindow()` for graceful shutdown

---

### Example 5: Using ProcessStartInfo
**Concept:** Advanced process configuration  
**Use Case:** When you need more control over process startup

```csharp
ProcessStartInfo startInfo = new ProcessStartInfo
{
    FileName = @"C:\Windows\notepad.exe",
    Arguments = @"C:\Windows\win.ini"
};
Process.Start(startInfo);
```

**Key Points:**
- `ProcessStartInfo` provides configuration options
- More flexible than simple `Process.Start()`
- Foundation for advanced scenarios

---

### Example 6: Controlling Process Priority
**Concept:** CPU scheduling priority  
**Use Case:** Background tasks, performance optimization

```csharp
process.PriorityClass = ProcessPriorityClass.BelowNormal;
```

**Priority Levels (lowest to highest):**
1. `Idle` - Only runs when system is idle
2. `BelowNormal` - Lower than normal priority
3. `Normal` - Default priority
4. `AboveNormal` - Higher than normal priority
5. `High` - High priority (use carefully)
6. `RealTime` - Highest priority (admin rights required)

**Best Practices:**
- Use `BelowNormal` or `Idle` for background tasks
- Avoid `RealTime` unless absolutely necessary
- `High` priority can starve other processes

---

### Example 7: Running Processes Silently
**Concept:** Background execution without UI  
**Use Case:** Automation scripts, background services

```csharp
var startInfo = new ProcessStartInfo
{
    FileName = "cmd.exe",
    Arguments = "/c dir",
    CreateNoWindow = true,
    UseShellExecute = false
};
```

**Key Properties:**
- `CreateNoWindow = true` - No console window appears
- `UseShellExecute = false` - Required for redirection and CreateNoWindow
- Perfect for batch operations

---

### Example 8: Working with Environment Variables
**Concept:** Process-specific environment  
**Use Case:** Configuration, passing data to child processes

```csharp
startInfo.EnvironmentVariables["MY_VAR"] = "Hello, Environment!";
```

**Key Points:**
- Child process inherits parent's environment
- Can add/modify variables for child process only
- Doesn't affect parent process environment
- Useful for configuration without command-line args

---

### Example 9: Redirecting Standard Input
**Concept:** Programmatic input to processes  
**Use Case:** Automating interactive command-line tools

```csharp
process.Start();
using (StreamWriter writer = process.StandardInput)
{
    writer.WriteLine("echo Hello!");
    writer.WriteLine("dir");
}
```

**Key Points:**
- Allows sending commands to interactive programs
- Must set `RedirectStandardInput = true`
- Use `StreamWriter` for convenience
- Remember to close the writer when done

---

### Example 10: Redirecting Standard Output
**Concept:** Capturing process output  
**Use Case:** Logging, parsing command output

```csharp
startInfo.RedirectStandardOutput = true;
process.Start();
string output = process.StandardOutput.ReadToEnd();
```

**Key Points:**
- Must set `RedirectStandardOutput = true`
- Read before `WaitForExit()` to avoid deadlock
- Can also redirect `StandardError` for error messages
- Use `ReadToEnd()` for complete output

## 🎯 Common Patterns

### Pattern 1: Execute and Capture Output
```csharp
var startInfo = new ProcessStartInfo
{
    FileName = "cmd.exe",
    Arguments = "/c ipconfig",
    RedirectStandardOutput = true,
    UseShellExecute = false,
    CreateNoWindow = true
};

using (var process = Process.Start(startInfo))
{
    string output = process.StandardOutput.ReadToEnd();
    process.WaitForExit();
    return output;
}
```

### Pattern 2: Wait for Process with Timeout
```csharp
process.Start();
if (!process.WaitForExit(5000)) // 5 second timeout
{
    process.Kill();
    Console.WriteLine("Process timed out and was killed.");
}
```

### Pattern 3: Handle Both Output and Errors
```csharp
startInfo.RedirectStandardOutput = true;
startInfo.RedirectStandardError = true;

process.Start();
string output = process.StandardOutput.ReadToEnd();
string errors = process.StandardError.ReadToEnd();
process.WaitForExit();
```

## 🔍 Important Concepts

### UseShellExecute
- **`true`** (default): Uses Windows shell to start process
  - Can open documents with default program
  - Can't redirect I/O streams
  - Example: Opening a .txt file opens in Notepad automatically

- **`false`**: Directly starts the executable
  - Required for I/O redirection
  - Required for `CreateNoWindow`
  - Must specify executable path

### Process Exit Codes
```csharp
process.WaitForExit();
int exitCode = process.ExitCode;
// 0 typically means success
// Non-zero usually indicates an error
```


## 📚 Additional Resources

- [Microsoft Docs: Process Class](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process)
- [ProcessStartInfo Properties](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo)

## 🧪 Practice Exercises

1. Create a program that starts multiple processes and monitors their CPU usage
2. Build a simple task killer that terminates processes by name with user confirmation
3. Create a command executor that captures both output and errors
4. Implement a process watchdog that restarts a process if it crashes
5. Build a log analyzer that runs command-line tools and parses their output


---

**Happy Coding! 🚀**
