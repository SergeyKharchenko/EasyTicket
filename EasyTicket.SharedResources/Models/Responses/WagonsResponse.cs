using System.Collections.Generic;

namespace EasyTicket.SharedResources.Models.Responses {
    public class WagonsResponse {
        public ICollection<Wagon> Wagons { get; set; } = new List<Wagon>();

        public class Wagon {
            public int Number { get; set; }
            public string TypeCode { get; set; }
            public int FreePlaces { get; set; }
            public int CoachType { get; set; }
            public string CoachClass { get; set; }
            public decimal Price { get; set; }
        }
    }
}