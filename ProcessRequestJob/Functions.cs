using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace ProcessRequestJob {
    public class Functions {
        public static async Task ProcessRequests([TimerTrigger("00:10:00", RunOnStartup = true)] TimerInfo timerInfo, TextWriter log) {
            Console.WriteLine($"Checking started at {DateTime.Now}");
            var ticketChecker = new TicketChecker();
            ticketChecker.Check();
        }
    }
}