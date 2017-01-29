using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EasyTicket.Api.Infrastructure.Enums;

namespace EasyTicket.Api.Data.Models {
    public class PlaceRequest {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PassangerName { get; set; }
        [Required]
        public string PassangerEmail { get; set; }
        [Required]
        public WagonType WagonType { get; set; } = WagonType.Coupe;
        [Required]
        public WagonArea WagonArea { get; set; } = WagonArea.Any;
        [Required]
        public PlaceType PlaceType { get; set; } = PlaceType.LeftBottom;
        [Required]
        public string Date { get; set; }
        [Required]
        public string PlacesString { get; set; }
        [NotMapped]
        public int[] Places {
            get { return PlacesString.Split(';').Select(int.Parse).ToArray(); }
            set { PlacesString = string.Join(";", value); }
        }
    }
}