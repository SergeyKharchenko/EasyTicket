using System.Collections.Generic;

namespace EasyTicket.SharedResources.Models.Responses {
    public class PlacesResponse {
        public string PlaceType { get; set; }
        public ICollection<int> Places { get; set; } = new List<int>();
    }
}