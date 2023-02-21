using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class PokemonTypesController : Controller
    {
        private readonly StoreFrontContext _context;

        public PokemonTypesController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: PokemonTypes
        public async Task<IActionResult> Index()
        {
            var storeFrontContext = _context.PokemonTypes.Include(p => p.Pokemon).Include(p => p.Type);
            return View(await storeFrontContext.ToListAsync());
        }

        // GET: PokemonTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PokemonTypes == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonTypes
                .Include(p => p.Pokemon)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.PokemonTypeId == id);
            if (pokemonType == null)
            {
                return NotFound();
            }

            return View(pokemonType);
        }

        // GET: PokemonTypes/Create
        public IActionResult Create()
        {
            ViewData["PokemonId"] = new SelectList(_context.Pokemons, "PokemonId", "PokemonName");
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "TypeName");
            return View();
        }

        // POST: PokemonTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PokemonTypeId,TypeId,PokemonId")] PokemonType pokemonType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemonType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PokemonId"] = new SelectList(_context.Pokemons, "PokemonId", "PokemonName", pokemonType.PokemonId);
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "TypeName", pokemonType.TypeId);
            return View(pokemonType);
        }

        // GET: PokemonTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PokemonTypes == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonTypes.FindAsync(id);
            if (pokemonType == null)
            {
                return NotFound();
            }
            ViewData["PokemonId"] = new SelectList(_context.Pokemons, "PokemonId", "PokemonName", pokemonType.PokemonId);
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "TypeName", pokemonType.TypeId);
            return View(pokemonType);
        }

        // POST: PokemonTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PokemonTypeId,TypeId,PokemonId")] PokemonType pokemonType)
        {
            if (id != pokemonType.PokemonTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemonType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonTypeExists(pokemonType.PokemonTypeId))
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
            ViewData["PokemonId"] = new SelectList(_context.Pokemons, "PokemonId", "PokemonName", pokemonType.PokemonId);
            ViewData["TypeId"] = new SelectList(_context.Types, "TypeId", "TypeName", pokemonType.TypeId);
            return View(pokemonType);
        }

        // GET: PokemonTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PokemonTypes == null)
            {
                return NotFound();
            }

            var pokemonType = await _context.PokemonTypes
                .Include(p => p.Pokemon)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.PokemonTypeId == id);
            if (pokemonType == null)
            {
                return NotFound();
            }

            return View(pokemonType);
        }

        // POST: PokemonTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PokemonTypes == null)
            {
                return Problem("Entity set 'StoreFrontContext.PokemonTypes'  is null.");
            }
            var pokemonType = await _context.PokemonTypes.FindAsync(id);
            if (pokemonType != null)
            {
                _context.PokemonTypes.Remove(pokemonType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonTypeExists(int id)
        {
          return _context.PokemonTypes.Any(e => e.PokemonTypeId == id);
        }
    }
}
