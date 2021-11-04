using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EFDataAccessLibrary.Models;
using Microsoft.Extensions.Logging;

namespace EFDataAccessLibrary.DataAccess
{
    public class PeopleContextSeed
    {
        public static async Task SeedAsync(PeopleContext context, ILoggerFactory loggerFactory)
         {
             try
             {
                 if (!context.People.Any())
                 {
                     var peopleData = File.ReadAllText("../EFDataAccessLibrary/DataAccess/people.json");

                     var people = JsonSerializer.Deserialize<List<Person>>(peopleData);

                     foreach (var person in people)
                     {
                         await context.People.AddRangeAsync(person);
                     }

                     await context.SaveChangesAsync();
                 }
             }
             catch (Exception ex)
             {
                 var logger = loggerFactory.CreateLogger<PeopleContextSeed>();
                 logger.LogError(ex.Message);
             }
         }
    }
}