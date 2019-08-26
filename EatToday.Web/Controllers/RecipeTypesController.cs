using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EatToday.Web.Data;
using EatToday.Web.Data.Entities;

namespace EatToday.Web.Controllers
{
    public class RecipeTypesController : Controller
    {
        private readonly DataContext _context;

        public RecipeTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: RecipeTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RecipeTypes.ToListAsync());
        }

        // GET: RecipeTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeType = await _context.RecipeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeType == null)
            {
                return NotFound();
            }

            return View(recipeType);
        }

        // GET: RecipeTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecipeTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RecipeType recipeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipeType);
        }

        // GET: RecipeTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeType = await _context.RecipeTypes.FindAsync(id);
            if (recipeType == null)
            {
                return NotFound();
            }
            return View(recipeType);
        }

        // POST: RecipeTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] RecipeType recipeType)
        {
            if (id != recipeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeTypeExists(recipeType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipeType);
        }

        // GET: RecipeTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeType = await _context.RecipeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeType == null)
            {
                return NotFound();
            }

            return View(recipeType);
        }

        // POST: RecipeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeType = await _context.RecipeTypes.FindAsync(id);
            _context.RecipeTypes.Remove(recipeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeTypeExists(int id)
        {
            return _context.RecipeTypes.Any(e => e.Id == id);
        }
    }
}
