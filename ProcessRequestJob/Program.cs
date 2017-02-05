using System;
using Microsoft.Azure.WebJobs;

namespace ProcessRequestJob {
    internal class Program {
        private static void Main(string[] args) {
            var config = new JobHostConfiguration();
            config.UseTimers();
            var host = new JobHost(config);
            //host.CallAsync(typeof(Functions).GetMethod("ProcessMethod"));
            host.RunAndBlock();
        }
    }
}