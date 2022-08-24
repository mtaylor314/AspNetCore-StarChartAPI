using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var co = _context.CelestialObjects.FirstOrDefault(c => c.Id == id);

            if (null == co) return NotFound();

            co.Satellites = co.Satellites.Where(c => c.OrbitedObjectId == id).ToList();

            return Ok(co);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var co = _context.CelestialObjects.FirstOrDefault(c => c.Name == name);

            if (null == co) return NotFound();

            co.Satellites = co.Satellites.Where(c => c.OrbitedObjectId == co.Id).ToList();

            return Ok(co);
        }

        public IActionResult GetAll()
        {
            var co = _context.CelestialObjects.ToList();



            return Ok(co);
        }
    }
}
