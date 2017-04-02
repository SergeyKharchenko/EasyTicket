using System.ComponentModel.DataAnnotations;

namespace EasyTicket.Api.Dto {
    public class TokenDto {
        [Required]
        public string Token { get; set; }
    }
}