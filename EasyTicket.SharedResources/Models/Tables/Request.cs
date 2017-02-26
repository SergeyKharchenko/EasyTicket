using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EasyTicket.SharedResources.Enums;

namespace EasyTicket.SharedResources.Models.Tables {
    public class Request {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StationFromId { get; set; }
        [Required]
        public string StationFromTitle { get; set; }
        [Required]
        public int StationToId { get; set; }
        [Required]
        public string StationToTitle { get; set; }
        [Required]
        public string PassangerName { get; set; }
        [Required]
        public string PassangerSurname { get; set; }
        [Required]
        [EmailAddress]
        public string PassangerEmail { get; set; }
        [Required]
        public WagonType WagonType { get; set; } = WagonType.Coupe;
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string PlacesString { get; set; }
        [NotMapped]
        public int[] Places {
            get { return PlacesString.Split(';').Select(int.Parse).ToArray(); }
            set { PlacesString = string.Join(";", value); }
        }
        [Required]
        public RequestState State { get; set; }
        [Required]
        public SearchType SearchType { get; set; }

        public override string ToString() {
            return $"{nameof(StationFromTitle)}: {StationFromTitle}, {nameof(StationToTitle)}: {StationToTitle}, {nameof(PassangerName)}: {PassangerName}, {nameof(PassangerSurname)}: {PassangerSurname}, {nameof(PassangerEmail)}: {PassangerEmail}, {nameof(WagonType)}: {WagonType}, {nameof(DateTime)}: {DateTime}, {nameof(PlacesString)}: {PlacesString}, {nameof(Places)}: {Places}, {nameof(State)}: {State}, {nameof(SearchType)}: {SearchType}";
        }
    }
}