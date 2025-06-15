using Event_Management_System.Hubs;
using Event_Management_System.Models.Interfaces;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace Event_Management_System.Controllers
{
    [Authorize(Policy = "UserOnly")]
    public class UserController : Controller
    {


        private readonly UserManager<MyAppUser> _user;
        private readonly IEventRepository _eventRepository;
        private readonly IHubContext<EventHub> _hubContext;


        public UserController(IEventRepository eventRepository, IHubContext<EventHub> hubContext, UserManager<MyAppUser> user)
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

        [Authorize(Policy = "UserOnly")]
        public IActionResult User_Dashboard()
        {
            return View("User_Dashboard");
        }


        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        public IActionResult Discover()
        {
            var obj = _eventRepository.GetEvents();
            if (obj != null)
            {
                return View("Discover_Event", obj);
            }
            TempData["Message"] = $"Unfortunately we have no events in our System";
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        public IActionResult EventTicket()
        {
            return RedirectToAction("Discover");
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        public IActionResult Book(string eventname)
        {
            TempData["EventName"] = eventname;
            return RedirectToAction("Ticket");
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")]
        public IActionResult Ticket()
        {
            ViewBag.EventName = TempData["EventName"];
            return View("BooKTicket");
        }

        [HttpPost]
        [Authorize(Policy = "UserOnly")]
        public IActionResult Ticket(Ticket model)
        {
            var result = _eventRepository.Ticket(model);
            if (result > 0)
            {
                TempData["Message"] = $"Congratulation your ticket for {model.Eventname} is Confirmed, See You there :)";
                return RedirectToAction("Confirm");
            }
            TempData["Message"] = $"Unfortunately unable to book ticket for {model.Eventname} ";
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")  ]
        public IActionResult Confirm()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "UserOnly")  ]
        public IActionResult Error()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
    }
}
