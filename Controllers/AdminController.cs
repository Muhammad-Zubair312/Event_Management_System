using Event_Management_System.Models;
using Event_Management_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Event_Management_System.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreGeneratedDocument;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Event_Management_System.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {

        private readonly UserManager<MyAppUser> _user;
        private readonly IEventRepository _eventRepository;
        private readonly IHubContext<EventHub> _hubContext;

        public AdminController(IEventRepository eventRepository, IHubContext<EventHub> hubContext, UserManager<MyAppUser> user)
        {
            _hubContext = hubContext;
            _eventRepository = eventRepository;
            _user = user;
        }
        
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult Privacy()
        {

            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public IActionResult Add()
        {   
                return View("Add_Event");
            
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> Add(Add_Event model)
        {
            
           var result = _eventRepository.Add(model);
            if(result > 0)
            {
                await _hubContext.Clients.All.SendAsync("EventAdd", model);

                TempData["Message"] = $"We are glad to onboard you D: Congratulation Your Event {model.Eventname} is successfully added to our System";

                return RedirectToAction("Confirm");
            }
            TempData["Message"] = $"Unfortunately Your Event {model.Eventname} is not added to our System";

            return RedirectToAction("Error");

        }


        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit()
        {
            var model = _eventRepository.GetEvents();
            return View("Edit_Event",model);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int eventSelect,string Venue,string Date)
        {
            string mesg = "";
            var result = _eventRepository.Update(eventSelect,Venue,Date);
            if (result)
            {
                var myevent = _eventRepository.GetById(eventSelect);
                await _hubContext.Clients.All.SendAsync("EventUpdate", myevent);

                mesg = $"Congratulation the Event date and venue is Updated Successfully";
                TempData["Message"] = mesg ;
                return RedirectToAction("Confirm");
            }
            mesg = $"Unfortunately Error when you Update the Event date and venue";
            ViewBag.Data = mesg ;
            return View("Error");
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete()
        {
            var model = _eventRepository.GetEvents();
            return View("Delete_Event",model);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task <IActionResult> Delete(int EventToDelete)
        {
            string mesg = "";
            var result = _eventRepository.Delete(EventToDelete);
            if (result)
            {
                var myevent = _eventRepository.GetById(EventToDelete);
                await _hubContext.Clients.All.SendAsync("EventDelete",myevent);
                mesg = $"Congratulation the Event Id {EventToDelete}  is Deleted Successfully";
                TempData["Message"] = mesg;
                return RedirectToAction("Confirm");
            }

            mesg = $"Unfortunately Error occur when you delete the Event with Id {EventToDelete}";
            ViewBag.Data = mesg;
            return View("Error");
        }


        [HttpGet]
        public IActionResult Confirm()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Admin_Dashboard()
        {
            return View();
        }

    
    }
}


//public async Task<IActionResult> DeleteAllUsers()
//{
//    var users = _user.Users.ToList(); // Gets all users from DB

//    foreach (var user in users)
//    {


//        var claims = await _user.GetClaimsAsync(user);
//        var userTypeClaim = claims.FirstOrDefault(c => c.Type == "Usertype");

//        if (userTypeClaim != null && userTypeClaim.Value == "Admin")
//        {
//            continue; // Skip Admin
//        }

//        var result = await _user.DeleteAsync(user);
//        if (!result.Succeeded)
//        {
//            // Handle deletion errors (optional)
//            foreach (var error in result.Errors)
//            {
//                ModelState.AddModelError(string.Empty, error.Description);
//            }
//        }
//    }

//            return RedirectToAction("Index"); 
//      }

