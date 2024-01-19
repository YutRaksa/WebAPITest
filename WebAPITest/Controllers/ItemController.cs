using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly ItemContext _context;
        public ItemController(ItemContext context)
        {
            _context = context;
        }
        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }
        // GET: api/Items
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModel>> GetItem(int id)
        {
            var Product = await _context.Items.FindAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Product;
        }
        // PUT: api/Items
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, ItemModel Item)
        {
            if (id != Item.Id)
            {
                return BadRequest();
            }
            _context.Entry(Item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // POST: api/Items
        [HttpPost]
        public async Task<ActionResult<ItemModel>> PostItem(ItemModel Item)
        {
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = Item.Id }, Item);
        }
        // DELETE: api/Items
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Item = await _context.Items.FindAsync(id);
            if (Item == null)
            {
                return NotFound();
            }
            _context.Items.Remove(Item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
