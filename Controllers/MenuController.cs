using Anthemis.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anthemis.Controllers{
    public class MenuController : Controller{
        private readonly AppDbContext _context;

        public MenuController(AppDbContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(string? category){
            var categories = await _context.MenuItems.Select(m => m.Category).Distinct().OrderBy(c => c).ToListAsync();

            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = category;

            var query = _context.MenuItems.AsQueryable();

            if (!string.IsNullOrEmpty(category)){
                query = query.Where(m => m.Category == category);
            }

            var menuItems = await query.OrderBy(m => m.Category).ThenBy(m => m.Name).ToListAsync();

            return View(menuItems);
        }
    }
}