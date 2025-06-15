using System.ComponentModel.DataAnnotations;

namespace Event_Management_System.Models
{
    public class Add_Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Eventname { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Venue { get; set; }

        public string Note { get; set; }

    }
}
