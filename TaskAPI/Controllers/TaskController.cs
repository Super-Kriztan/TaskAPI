using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using TaskAPI.MODELS;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbContext;


        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetBrands()
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            return await _dbContext.Brands.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetBrand(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }
        [HttpPost]
        public async Task<ActionResult<TaskModel>> PostBrand(TaskModel brand)
        {
            _dbContext.Brands.Add(brand);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrand), new { id = brand.Id }, brand);
        }
        [HttpPut]
        public async Task<IActionResult> PutBrand(int id, TaskModel brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(brand).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return Ok();
        }
        private bool BrandAvailable(int id)
        {
            return (_dbContext.Brands?.Any(x => x.Id == id)).GetValueOrDefault();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _dbContext.Brands.Remove(brand);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }


    }
}