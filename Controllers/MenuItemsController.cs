using Anthemis.Data;
using Anthemis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anthemis.Controllers{
    public class MenuItemsController : Controller{
        private readonly AppDbContext _context;

        public MenuItemsController(AppDbContext context){
            _context = context;
        }

        private bool IsAdmin(){
            return HttpContext.Session.GetString("Role") == "Admin";
        }

        public async Task<IActionResult> Index(){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            var menuItems = await _context.MenuItems.OrderBy(m => m.Category).ThenBy(m => m.Name).ToListAsync();

            return View(menuItems);
        }

        [HttpGet]
        public IActionResult Create(){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid){
                return View(menuItem);
            }

            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            var menuItem = await _context.MenuItems.FindAsync(id);

            if (menuItem == null){
                return NotFound();
            }

            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItem menuItem){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            if (id != menuItem.Id){
                return NotFound();
            }

            if (!ModelState.IsValid){
                return View(menuItem);
            }

            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            var menuItem = await _context.MenuItems.FindAsync(id);

            if (menuItem == null){
                return NotFound();
            }

            return View(menuItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id){
            if (!IsAdmin()){
                return RedirectToAction("Login", "Account");
            }

            var menuItem = await _context.MenuItems.FindAsync(id);

            if (menuItem != null){
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}