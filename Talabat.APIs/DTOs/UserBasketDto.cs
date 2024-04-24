using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class UserBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
