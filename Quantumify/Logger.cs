using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Raylib_cs;
using Riptide.Utils;

namespace Quantumify;

public static class Logger
{

    public delegate bool OnMessage(LogType type, string message, int skipFrames,ConsoleColor color);
    public static event OnMessage? Message;
    
    private static string TimeStamp()
    {
        return $"[{DateTime.Now.ToString("HH:mm")}] ";
    }

    private static void Log(string message, string prefix = "", ConsoleColor color = ConsoleColor.White, int skipFrames = 3)
    {

        StackTrace stackTrace = new StackTrace(skipFrames,true);
        StackFrame stackFrame = new StackFrame(skipFrames);
        string methodName = stackFrame.GetMethod().DeclaringType.ToString();
        int line = stackTrace.GetFrame(0).GetFileLineNumber();

        if (Message != null)
        {
            if (Message.Invoke(LogType.Debug, message, skipFrames, color))
            {
                return;
            }
        }
        
        Console.ResetColor();
        Console.ForegroundColor = color;
        Console.WriteLine($"[{methodName}:{line}]{TimeStamp()}[{prefix}]: {message}");
        Console.ResetColor();
    }

    public static void Debug(string message, int skipFrames = 2)
    {
#if(DEBUG)
        Log(message, "DEBUG", ConsoleColor.Gray, skipFrames);
#endif
    }
    public static void Info(string message, int skipFrames = 2)=> Log(message, "INFO", ConsoleColor.DarkBlue, skipFrames);
    public static void Warn(string message, int skipFrames = 2)=>Log(message, "INFO", ConsoleColor.Yellow, skipFrames);
    public static void Error(string message, int skipFrames = 2) => Log(message,"ERROR",ConsoleColor.Red,skipFrames);
    
    public static void Fatal(string message, int skipFrames = 2)=>  Log(message, "FATAL", ConsoleColor.Cyan, skipFrames);
    
    /// <summary>
    /// Configures a custom <see cref="Raylib"/> log by setting a trace log callback.
    /// </summary>
    internal static unsafe void SetupRaylibLogger() {
        Raylib.SetTraceLogCallback(&RaylibLogger);
    }
    
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static unsafe void RaylibLogger(int logLevel, sbyte* text, sbyte* args) {
        string message = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));

        switch ((TraceLogLevel)logLevel)
        {
            case TraceLogLevel.Debug:
                Debug(message,3);
                break;

            case TraceLogLevel.Info:
                Info(message,3);
                break;
            
            case TraceLogLevel.Warning:
                Warn(message,3);
                break;
            
            case TraceLogLevel.Error:
                Error(message,3);
                break;
            
            case TraceLogLevel.Fatal:
                Fatal(message,3);
                break;
        }
    }
}
