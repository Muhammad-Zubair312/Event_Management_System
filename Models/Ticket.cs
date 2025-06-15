using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Models
{
    public class Ticket
    {

        [Key]
        public int Id { get; set; }

        public string Eventname { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public int Number_of_Tickets { get; set; }

    }
}
