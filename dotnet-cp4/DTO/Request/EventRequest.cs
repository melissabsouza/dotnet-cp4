

using System.ComponentModel.DataAnnotations;

namespace dotnet_cp4.DTO.Request
{
    public class EventRequest
    {
        [Required]
        public string Name { get; set; }
        public DateOnly EventDate { get; set; }
        [Required]
        public float TicketPrice { get; set; }
        [Required]
        public string Local { get; set; }
    }
}
