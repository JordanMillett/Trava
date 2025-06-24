using System.Runtime.CompilerServices;

namespace Trava.Blazor.Services.Server;

public class IServerLogger
{
    public enum LogSource
    {
        User,
        System,
        Warning,
        Error,
        Context
    }

    private readonly DateTime StartTime;
    private int Activity = 0;
    private Timer? pulseTimer;
    private readonly TimeSpan pulseInterval = TimeSpan.FromHours(4);
    
    private readonly List<string> AllLogs = new(); 
    private readonly int MaxLogs = 1000;

    public IServerLogger()
    {
        StartTime = DateTime.UtcNow;
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;

        SchedulePulse();
    }

    private void SchedulePulse()
    {
        pulseTimer = new Timer(_ =>
        {
            try
            {
                Pulse();
            }
            catch{}
        }, null, TimeSpan.Zero, pulseInterval);
    }

    public void Pulse()
    {
        TimeSpan uptime = DateTime.UtcNow - StartTime;
        string uptimeFormatted = $"{uptime.Days}d {uptime.Hours:D2}h {uptime.Minutes:D2}m";

        Log($"Server Uptime: {uptimeFormatted} - Activity Recorded: {Activity}x", LogSource.Context);
        Activity = 0;
    }

    public void Log<T>(T obj, LogSource source = LogSource.User)
    {
        if(obj == null || string.IsNullOrWhiteSpace(obj.GetType().Name))
        {
            LogWarning($"Type '{typeof(T).Name}' was unable to log due to being null.");
        }else
        {
            Log(obj.ToString()!, source);
        }
    }

    public void LogWarning<T>(T obj,
    [CallerFilePath] string file = "",
    [CallerLineNumber] int line = 0,
    [CallerMemberName] string function = "")
    {
        if(obj == null || string.IsNullOrWhiteSpace(obj.GetType().Name))
        {
            LogWarning($"Type '{typeof(T).Name}' was unable to log due to being null.");
        }else
        {
            LogWarning(obj.ToString()!, file, line, function);
        }
    }
    
    public void Log(string message, LogSource source = LogSource.User)
    {
        Console.ForegroundColor = source switch
        {
            LogSource.User => ConsoleColor.White,
            LogSource.System => ConsoleColor.Green,
            LogSource.Warning => ConsoleColor.Yellow,
            LogSource.Error => ConsoleColor.Red,
            LogSource.Context => ConsoleColor.DarkGray,
            _ => ConsoleColor.Red
        };

        string prefix = source switch
        {
            LogSource.User => "Txt",
            LogSource.System => "Sys",
            LogSource.Warning => "Wrn",
            LogSource.Error => "Err",
            LogSource.Context => "Inf",
            _ => "???"
        };

        string time = DateTime.UtcNow.ToString(@"hh\:mm\:ss\:fff");
        string line = $"[{time}]\t({prefix})\t{message}";

        lock (AllLogs)
        {
            AllLogs.Insert(0, line);
            if (AllLogs.Count > MaxLogs)
                AllLogs.RemoveAt(AllLogs.Count - 1);
        }

        Console.WriteLine(line);
        Console.ForegroundColor = ConsoleColor.White;
        Activity++;
    }

    public void LogWarning(string message,
    [CallerFilePath] string file = "",
    [CallerLineNumber] int line = 0,
    [CallerMemberName] string function = "")
    {
        string path = Path.GetFileName(file);
        Log($"{message}", LogSource.Warning);
        Log($"^ {path}({line},1) -> {function}()", LogSource.Context);
    }

    public void LogError(Exception ex,
    [CallerFilePath] string file = "",
    [CallerLineNumber] int line = 0,
    [CallerMemberName] string function = "")
    {
        string path = Path.GetFileName(file);
        Log(ex.Message, LogSource.Error);
        Log($"^ {path}({line},1) -> {function}()", LogSource.Context);
    }

    public IReadOnlyList<string> GetLogs()
    {
        lock (AllLogs)
        {
            return AllLogs.AsReadOnly();
        }
    }
}