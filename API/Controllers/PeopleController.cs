using System.Linq;
using System.Threading.Tasks;
using EFDataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// Benefits of Entity Framework Core
// 1. Faster development speed
// 2. You don't have to know SQL

// Benefits of Dapper
// 1. Faster in production
// 2. Easier to work with for SQL developer
// 3. Design for loose coupling

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
        public object GetPeople()
        {
            var people = _db.People
                .Include(a => a.Addresses)
                .Include(e => e.EmailAddresses)
                // .Where(x => ApprovedAge(x.Age)) // don't do this because it will query in sql not in c#
                .Where(x => x.Age >= 18 && x.Age <= 65) // do this instead
                .ToList();

            return people;
        }

        private bool ApprovedAge(int age)
        {
            return (age >= 18 && age <= 65);
        }
    }
}