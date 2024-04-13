using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace KaganoEngine;

public class Logger
{
    public static void Init()
    {
        
    }

    private static string TimeStamp()
    {
        return $"[{DateTime.Now.ToString("yyyy-MM-dd|HH:mm:ss")}] ";
    }

    private static void Log(string message, string prefix = "", ConsoleColor color = ConsoleColor.White, int skipFrames = 3)
    {

        StackTrace stackTrace = new StackTrace(skipFrames,true);
        StackFrame stackFrame = new StackFrame(skipFrames);
        string methodName = stackFrame.GetMethod().DeclaringType.ToString();
        int line = stackTrace.GetFrame(0).GetFileLineNumber();
        
        Console.ResetColor();
        Console.ForegroundColor = color;
        Console.WriteLine($"{methodName}:{line} {prefix}: {message}");
        Console.ResetColor();
    }

    public static void Warning(string message, int skipFrames = 2)
    {
        Log(message,"WARNING",ConsoleColor.Yellow,skipFrames);
    }

    public static void Error(string message, int skipFrames = 2)
    {
        Log(message,"ERROR",ConsoleColor.Red,skipFrames);
    }

    public static void Success(string message, int skipFrames = 2)
    {
        Log(message, "SUCCESS", ConsoleColor.Green, skipFrames);
    }

    public static void Info(string message, int skipFrames = 2)
    {
        Log(message, "INFO", ConsoleColor.Cyan, skipFrames);
    }
    
    public static void Fatal(string message, int skipFrames = 2)
    {
        Log(message, "INFO", ConsoleColor.Cyan, skipFrames);
    }
    
    public static void Debug(string message, int skipFrames = 2)
    {
        Log(message, "INFO", ConsoleColor.Cyan, skipFrames);
    }

    public static void Warn(string message, int skipFrames = 2)
    {
        Log(message, "INFO", ConsoleColor.Cyan, skipFrames);
    }
    /// <summary>
    /// Configures a custom <see cref="Raylib"/> log by setting a trace log callback.
    /// </summary>
    internal static unsafe void SetupRaylibLogger() {
        Raylib.SetTraceLogCallback(&RaylibLogger);
    }
    
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static unsafe void RaylibLogger(int logLevel, sbyte* text, sbyte* args) {
        string message = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));
        
        switch ((TraceLogLevel) logLevel) {
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
