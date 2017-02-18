using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyTicket.SharedResources;
using EasyTicket.SharedResources.Models;
using EasyTicket.SharedResources.Models.Tables;
using Microsoft.Azure.WebJobs;

namespace ProcessRequestJob {
    public class Functions {
        public static async Task ProcessRequests([TimerTrigger("00:00:05", RunOnStartup = true)] TimerInfo timerInfo, TextWriter log) {
            using (var dbContext = new UzDbContext()) {
                if (!dbContext.Requests.Any()) {
                    return;
                }

                Console.WriteLine($"There are {dbContext.Requests.Count()} requests in database:");
                foreach (Request request in dbContext.Requests) {
                    Console.WriteLine($"\tFor {request.PassangerEmail} on {request.DateTime}");
                }
            }
            //Console.WriteLine("--------------------------------------------------------------------");
            //foreach (ConnectionStringSettings connectionStringSettings in ConfigurationManager.ConnectionStrings) {
            //    Console.WriteLine($"{connectionStringSettings.Name}{connectionStringSettings.ConnectionString}");
            //}
            //Console.WriteLine("--------------------------------------------------------------------");
        }
    }
}