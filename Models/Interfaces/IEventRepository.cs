using Microsoft.AspNetCore.Authentication;

namespace Event_Management_System.Models.Interfaces
{
    public interface IEventRepository
    {
        public int Add(Add_Event obj);
        public int Ticket(Ticket ticket);

        //public void Delete(Ticket ticket);

        public bool Update(int id,string venue,string date);
    
        public string GetName(int id);

        public Add_Event GetById(int id);
        public bool Delete(int id);
       public List<Add_Event> GetEvents();

    }
}
