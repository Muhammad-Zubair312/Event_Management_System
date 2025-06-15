using Event_Management_System.Models;
using Event_Management_System.Models.Interfaces;
using Humanizer.Localisation;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Event_Management_System.Models.Repositories
{
    public class EventRepository : IEventRepository
    {

        private readonly MyAppDB db = new MyAppDB();

        
        public int Add(Add_Event obj)
        {
            int result;
            db.Add_Event.Add(obj);
            result = db.SaveChanges();
            return result;
        }

        public int Ticket(Ticket ticket)
        {
            int result;
            db.Ticket.Add(ticket);
            result = db.SaveChanges();
            return result;
        }

        public List<Add_Event> GetEvents()
        {
            return db.Add_Event.ToList();

        }

        public bool Update(int id,string venue,string date)
        {
           var myev = db.Add_Event.FirstOrDefault(e => e.Id == id);
            if (myev != null)
            {  
                myev.Venue = venue;
                myev.Date = date;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id) {
            var myev = db.Add_Event.FirstOrDefault(e => e.Id == id);
            if (myev != null)
            {
                db.Add_Event.Remove(myev);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public string GetName(int id)
        {
            var myev = db.Add_Event.FirstOrDefault(e => e.Id == id);
            if(myev != null)
            {
                return myev.Name;
            }

            return "N/A";
        }

        public Add_Event GetById(int id)
        {
            return db.Add_Event.FirstOrDefault(e => e.Id == id);
        }
    }
 }



//   public void Ticket(Ticket ticket)
//{
//    db.Ticket.Add(ticket);
//    db.SaveChanges();

//}


// Add a new student to the database
//public void Add(Student student)
//{
//    db.Students.Add(student);
//    db.SaveChanges();
//}

// Get the first student from the database
//public Student GetFirstStudent()
//{
//    Student stu = db.Students.FirstOrDefault(); // Use FirstOrDefault to avoid exception if table is empty
//    return stu;
//}

//// Update the first student's name to "Ahmed"
//public void UpdateFirstStudent()
//{
//    Student student = db.Students.FirstOrDefault();
//    if (student != null)
//    {
//        student.Name = "Ahmed";
//        db.SaveChanges();
//    }
//}

//// Delete the first student from the database
//public void DeleteFirstStudent()
//{
//    Student student = db.Students.FirstOrDefault();
//    if (student != null)
//    {
//        db.Students.Remove(student);
//        db.SaveChanges();
//    }
//}
//    }
//}
