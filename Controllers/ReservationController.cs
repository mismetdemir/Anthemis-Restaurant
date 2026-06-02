using Anthemis.Data;
using Anthemis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anthemis.Controllers{
    public class ReservationController : Controller{
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context){
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(){
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null){
                return RedirectToAction("Login", "Account");
            }

            var model = new ReservationViewModel{
                ReservationDate = DateTime.Today,
                ReservationTime = new TimeSpan(19, 0, 0),
                GuestCount = 2
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ReservationViewModel model){
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null){
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid){
                return View(model);
            }

            var reservation = new Reservation{
                UserId = userId.Value,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                ReservationDate = model.ReservationDate,
                ReservationTime = model.ReservationTime,
                GuestCount = model.GuestCount,
                Note = model.Note,
                Status = "Pending"
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Your reservation has been created successfully.";

            return RedirectToAction("MyReservations");
        }

        public async Task<IActionResult> MyReservations(){
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null){
                return RedirectToAction("Login", "Account");
            }

            var reservations = await _context.Reservations.Where(r => r.UserId == userId.Value).ToListAsync();

            reservations = reservations.OrderByDescending(r => r.ReservationDate).ThenBy(r => r.ReservationTime).ToList();

            return View(reservations);
        }
    }
}