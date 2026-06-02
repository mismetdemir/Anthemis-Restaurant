using Anthemis.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anthemis.Controllers{
    public class AdminController : Controller{
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context){
            _context = context;
        }

        private bool IsAdmin(){
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        [HttpGet]
        public IActionResult Index(){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reservations(){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            var reservations = await _context.Reservations.ToListAsync();

            reservations = reservations.OrderByDescending(r => r.ReservationDate).ThenBy(r => r.ReservationTime).ToList();

            return View(reservations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReservationStatus(int id, string status){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            var allowedStatuses = new[] { "Pending", "Approved", "Cancelled" };

            if (!allowedStatuses.Contains(status)){
                return BadRequest();
            }

            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null){
                return NotFound();
            }

            reservation.Status = status;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reservation status updated successfully.";

            return RedirectToAction(nameof(Reservations));
        }
    }
}