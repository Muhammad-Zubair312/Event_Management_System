using Microsoft.AspNetCore.Identity;

namespace Event_Management_System.Models
{
    public class MyAppUser:IdentityUser
    {

        public string City {get; set;}

        public string State {get; set;}

        }
}
