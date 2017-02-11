using System;
using System.Globalization;
using System.Threading.Tasks;
using EasyTicket.SharedResources;

namespace EasyTicket.Workbench {
    public static class Client {
        public static async Task<string> Wagons() {
            var uzClient = new UzClient();
            UzContext context = await uzClient.GetUZContext();
            string wagons =
                await uzClient.GetWagons(context,
                                         stationIdFrom: 2200001, // Киев
                                         stationIdTo: 2210900,   // Кривой Рог - Главный
                                         date: DateTime.ParseExact("2017-02-13 17:24:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                                         trainId: "740Ш",
                                         trainType: 5,
                                         wagonType: "С2");
            return wagons;
        }
    }
}