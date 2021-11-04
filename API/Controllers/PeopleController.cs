using System.Threading.Tasks;
using EFDataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class PeopleController : BaseApiController
    {
        private readonly PeopleContext _db;
        private readonly ILogger<PeopleController> _logger;
        public PeopleController(PeopleContext db, ILogger<PeopleController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetPeople()
        {
            var people = await _db.People
                .Include(a => a.Addresses)
                .Include(e => e.EmailAddresses)
                .ToListAsync();

            return Ok(people);
        }
    }
}