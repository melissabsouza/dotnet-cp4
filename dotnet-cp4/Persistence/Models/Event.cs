using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_cp4.Persistence.Models
{
    [Table("CP4_EVENT")]
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        [Required]
        public float TicketPrice { get; set; }
        [Required]
        public string Local { get; set; }
    }
}
