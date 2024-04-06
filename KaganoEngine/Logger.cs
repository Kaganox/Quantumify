using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaganoEngine;

public class Logger
{

    private static string TimeStamp()
    {
        return $"[{DateTime.Now.ToString("yyyy-MM-dd|HH:mm:ss")}] ";
    }

    public static void Log(string message,string prefix = "",bool changeColor = true)
    {
        if (changeColor) { Console.ResetColor(); }
        Console.WriteLine($"{TimeStamp()} {prefix} {message}");
    }

    public static void Warning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Log(message,"WARNING",false);
    }

    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Log(message,"ERROR",true);
    }

    public static void Success(string message)
    {
        Console.BackgroundColor = ConsoleColor.Green;
        Log(message,"SUCCESS",true);
    }

    public static void Info(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Log(message,"INFO",true);
    }
}
