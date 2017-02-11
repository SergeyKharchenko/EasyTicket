using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTicket.SharedResources;

namespace EasyTicket.Workbench {
    class Program {
        static void Main(string[] args) {
            string wagons = Client.Wagons().Result;
            Console.WriteLine(wagons);
            Console.ReadKey();
        }


    }
}
