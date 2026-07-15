using Microsoft.AspNetCore.Mvc;
using WrightBrothersApi.Models;

namespace WrightBrothersApi.Controllers
{
    /// <summary>Manages plane-related API endpoints using an in-memory collection.</summary>
    [ApiController]
    [Route("[controller]")]
    public class PlanesController : ControllerBase
    {
        /// <summary>Logger used for request and operation tracing.</summary>
        private readonly ILogger<PlanesController> _logger;

        /// <summary>Initializes a new instance of the controller with logger dependency.</summary>
        public PlanesController(ILogger<PlanesController> logger) { _logger = logger; }

        /// <summary>In-memory data source containing Wright Brothers plane records.</summary>
        private static readonly List<Plane> Planes = new List<Plane>
        {
            new Plane { Id = 1, Name = "Wright Flyer", Year = 1903, Description = "The first successful heavier-than-air powered aircraft.", RangeInKm = 12, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Wright_Model_B.jpg/800px-Wright_Model_B.jpg" },
            new Plane { Id = 2, Name = "Wright Flyer II", Year = 1904, Description = "A refinement of the original Flyer with better performance.", RangeInKm = 24, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Wright_Model_B.jpg/800px-Wright_Model_B.jpg" },
            new Plane { Id = 3, Name = "Wright Model A", Year = 1908, Description = "The first commercially successful airplane.", RangeInKm = 40, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Wright_Model_B.jpg/800px-Wright_Model_B.jpg" },
            new Plane { Id = 4, Name = "Wright Model B", Year = 1910, Description = "An improved version of the Model A with better stability.", RangeInKm = 80, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Wright_Model_B.jpg/800px-Wright_Model_B.jpg" },
            new Plane { Id = 5, Name = "Wright Model C", Year = 1912, Description = "The first aircraft with an enclosed fuselage and wheels.", RangeInKm = 120, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Wright_Model_B.jpg/800px-Wright_Model_B.jpg" },
            new Plane { Id = 6, Name = "Wright Model D", Year = 1913, Description = "An advanced model with improved aerodynamics and control systems.", RangeInKm = 160, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Wright_Model_B.jpg/800px-Wright_Model_B.jpg" }
        };

        /// <summary>Creates a new plane if its ID does not already exist in the collection.</summary>
        [HttpPost]
        public ActionResult<Plane> Post(Plane plane)
        {
            /// <summary>Reject duplicate IDs to preserve uniqueness in the in-memory store.</summary>
            if (Planes.Exists(p => p.Id == plane.Id)) return BadRequest("Plane with the same ID already exists.");

            /// <summary>Add the plane and return location metadata for retrieval by ID.</summary>
            Planes.Add(plane);
            return CreatedAtAction(nameof(GetById), new { id = plane.Id }, plane);
        }

        /// <summary>Updates an existing plane by ID using values from the request body.</summary>
        [HttpPut("{id}")]
        public ActionResult<Plane> Put(int id, Plane updatedPlane)
        {
            /// <summary>Find target plane; return 404 if no matching record exists.</summary>
            var plane = Planes.Find(p => p.Id == id);
            if (plane == null) return NotFound();

            /// <summary>Apply all mutable fields from the incoming payload.</summary>
            plane.Name = updatedPlane.Name;
            plane.Year = updatedPlane.Year;
            plane.Description = updatedPlane.Description;
            plane.RangeInKm = updatedPlane.RangeInKm;
            plane.ImageUrl = updatedPlane.ImageUrl;

            /// <summary>Return 204 to indicate successful update with no response body.</summary>
            return NoContent();
        }

        /// <summary>Deletes a plane record by ID when present.</summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            /// <summary>Log delete intent for diagnostics and request auditing.</summary>
            _logger.LogInformation($"DELETE plane with ID: {id}");

            /// <summary>Find target plane; return 404 if no matching record exists.</summary>
            var plane = Planes.Find(p => p.Id == id);
            if (plane == null) return NotFound();

            /// <summary>Remove plane and return 204 to indicate successful deletion.</summary>
            Planes.Remove(plane);
            return NoContent();
        }

        /// <summary>Returns the first N planes from the collection.</summary>
        [HttpGet("count/{count}")]
        public ActionResult<List<Plane>> GetByCount(int count)
        {
            /// <summary>Take only requested number of records and return as list.</summary>
            var planes = Planes.Take(count).ToList();
            return Ok(planes);
        }

        /// <summary>Searches planes by name using case-insensitive partial matching.</summary>
        [HttpGet("search/{name}")]
        public ActionResult<List<Plane>> SearchByName(string name)
        {
            /// <summary>Filter planes where Name contains search text ignoring case.</summary>
            var planes = Planes.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(planes);
        }

        /// <summary>Gets one plane by its ID.</summary>
        [HttpGet("{id}")]
        public ActionResult<Plane> GetById(int id)
        {
            /// <summary>Find plane by ID and return 404 when it does not exist.</summary>
            var plane = Planes.Find(p => p.Id == id);
            if (plane == null) return NotFound();
            return Ok(plane);
        }
    }
}
