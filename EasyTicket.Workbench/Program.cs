using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyTicket.SharedResources;

namespace EasyTicket.Workbench {
    class Program {
        static void Main(string[] args) {
            Console.OutputEncoding = Encoding.UTF8;
            var jobEmulator = new JobEmulator();
            jobEmulator.Run();
            while (true) {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q) {
                    break;
                }
            }
            
        }

       
    }
}
