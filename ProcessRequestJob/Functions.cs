using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

public class Functions {
    public static async Task ProcessMethodValera([TimerTrigger("00:00:05", RunOnStartup = true)] TimerInfo timerInfo, TextWriter log) {
        Console.WriteLine(DateTime.Now);
    }
}