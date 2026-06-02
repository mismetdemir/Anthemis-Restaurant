using Anthemis.Data;
using Anthemis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anthemis.Controllers{
    public class HomeController : Controller{
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            var menuItems = await _context.MenuItems.ToListAsync();

            var todayNumber = DateTime.Today.DayOfYear;

            var dailyPicks = menuItems.GroupBy(m => m.Category).OrderBy(group => group.Key).Select((group, categoryIndex) => {
                var items = group.OrderBy(m => m.Id).ToList();
                var selectedIndex = (todayNumber + categoryIndex) % items.Count;

                return items[selectedIndex];
            }).ToList();

            var model = new HomeIndexViewModel{
                DailyPicks = dailyPicks
            };

            return View(model);
        }
    }
}