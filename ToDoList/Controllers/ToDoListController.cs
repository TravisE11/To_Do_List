using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Models;
using ToDoList.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly ILogger<ToDoListController> _logger;
        private readonly ToDoListContext _context;

        public ToDoListController(ILogger<ToDoListController> logger, ToDoListContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tasks = _context.SomeTask;
            foreach(var task in tasks){
                Console.WriteLine(task.Id);
                Console.WriteLine(task.Title);
            }
            return View(await _context.SomeTask.ToListAsync());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Title,Description,Status")] ToDoList.Models.SomeTask SomeTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(SomeTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(SomeTask);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var someTask = await _context.SomeTask.FindAsync(id);

            return View(someTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status")] ToDoList.Models.SomeTask SomeTask)
        {
            if (ModelState.IsValid)
            {
                _context.Update(SomeTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(SomeTask);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var someTask = await _context.SomeTask.FirstOrDefaultAsync(m => m.Id == id);

            return View(someTask);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var someTask = await _context.SomeTask.FindAsync(id);
            _context.SomeTask.Remove(someTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
