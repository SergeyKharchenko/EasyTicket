using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using EasyTicket.SharedResources.Models;

namespace EasyTicket.Api.Dto {
    public class RequestDto {
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
        public int[] Places { get; set; }
        [Required]
        public string Date { get; set; }
        public DateTime DateTime {
            get { return DateTime.ParseExact(Date, "dd.MM.yyyy", CultureInfo.InvariantCulture); }
            set { Date = value.ToString("dd.MM.yyyy"); }
        }
    }
}