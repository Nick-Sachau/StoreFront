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
    public class OrderPokemonController : Controller
    {
        private readonly StoreFrontContext _context;

        public OrderPokemonController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: OrderPokemon
        public async Task<IActionResult> Index()
        {
            var storeFrontContext = _context.OrderPokemons.Include(o => o.Order).Include(o => o.Pokemon);
            return View(await storeFrontContext.ToListAsync());
        }

        // GET: OrderPokemon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderPokemons == null)
            {
                return NotFound();
            }

            var orderPokemon = await _context.OrderPokemons
                .Include(o => o.Order)
                .Include(o => o.Pokemon)
                .FirstOrDefaultAsync(m => m.OrderPokemonId == id);
            if (orderPokemon == null)
            {
                return NotFound();
            }

            return View(orderPokemon);
        }

        // GET: OrderPokemon/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "ShipCity");
            ViewData["PokemonId"] = new SelectList(_context.Pokemons, "PokemonId", "PokemonName");
            return View();
        }

        // POST: OrderPokemon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderPokemonId,OrderId,Quantity,PokemonId,ProductPrice")] OrderPokemon orderPokemon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderPokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "ShipCity", orderPokemon.OrderId);
            ViewData["PokemonId"] = new SelectList(_context.Pokemons, "PokemonId", "PokemonName", orderPokemon.PokemonId);
            return View(orderPokemon);
        }

        // GET: OrderPokemon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderPokemons == null)
            {
                return NotFound();
            }

            var orderPokemon = await _context.OrderPokemons.FindAsync(id);
            if (orderPokemon == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "ShipCity", orderPokemon.OrderId);
            ViewData["PokemonId"] = new SelectList(_context.Pokemons, "PokemonId", "PokemonName", orderPokemon.PokemonId);
            return View(orderPokemon);
        }

        // POST: OrderPokemon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderPokemonId,OrderId,Quantity,PokemonId,ProductPrice")] OrderPokemon orderPokemon)
        {
            if (id != orderPokemon.OrderPokemonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderPokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderPokemonExists(orderPokemon.OrderPokemonId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "ShipCity", orderPokemon.OrderId);
            ViewData["PokemonId"] = new SelectList(_context.Pokemons, "PokemonId", "PokemonName", orderPokemon.PokemonId);
            return View(orderPokemon);
        }

        // GET: OrderPokemon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderPokemons == null)
            {
                return NotFound();
            }

            var orderPokemon = await _context.OrderPokemons
                .Include(o => o.Order)
                .Include(o => o.Pokemon)
                .FirstOrDefaultAsync(m => m.OrderPokemonId == id);
            if (orderPokemon == null)
            {
                return NotFound();
            }

            return View(orderPokemon);
        }

        // POST: OrderPokemon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderPokemons == null)
            {
                return Problem("Entity set 'StoreFrontContext.OrderPokemons'  is null.");
            }
            var orderPokemon = await _context.OrderPokemons.FindAsync(id);
            if (orderPokemon != null)
            {
                _context.OrderPokemons.Remove(orderPokemon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderPokemonExists(int id)
        {
          return _context.OrderPokemons.Any(e => e.OrderPokemonId == id);
        }
    }
}
