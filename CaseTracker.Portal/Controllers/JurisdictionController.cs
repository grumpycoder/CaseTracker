using System.Threading.Tasks;
using CaseTracker.Core.Models;
using CaseTracker.Data;
using CaseTracker.Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseTracker.Portal.Controllers
{
    [Route("api/[controller]")]
    public class JurisdictionController : Controller
    {
        private readonly AppDbContext context;
        public JurisdictionController(AppDbContext _context)
        {
            context = _context;
        }

        [HttpGet("list")]
        public async Task<object> List()
        {
            var list = await context.Jurisdictions.ToListAsync();
            return Ok(list);
        }

        [HttpPut()]
        public async Task<object> Put([FromBody]JurisdictionViewModel model)
        {
            if (model == null) return BadRequest("No Jurisdiction to update");

            var jurisdiction = await context.Jurisdictions.FindAsync(model.Id);
            if (jurisdiction == null) return NotFound("Jurisdiction not found");

            jurisdiction.Name = model.Name;
            jurisdiction.Abbreviation = model.Abbreviation;

            await context.SaveChangesAsync();
            return Ok(jurisdiction);
        }

        [HttpPost()]
        public async Task<object> Post([FromBody]JurisdictionViewModel model)
        {
            if (model == null) return BadRequest("No Jurisdiction to add");

            var jurisdiction = new Jurisdiction()
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation
            };
            await context.Jurisdictions.AddAsync(jurisdiction);
            await context.SaveChangesAsync();

            return Ok(jurisdiction);
        }

    }
}