using System.Collections.Generic;

namespace EasyTicket.SharedResources.Models.Responses {
    public class BookPlacesResponse {
        public bool IsError { get; set; } = true;
        public Dictionary<string, string> Cookies { get; set; }
    }
}