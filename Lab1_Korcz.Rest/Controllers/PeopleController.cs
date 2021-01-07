using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_Korcz.Rest.Context;
using Lab1_Korcz.Rest.Filters;
using Lab1_Korcz.Rest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lab1_Korcz.Rest.Controllers
{
    [ApiController][ApiKeyAuth]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly AzureDbContext db;
        public Person people;
        public PeopleController(AzureDbContext db)
        {
            this.db = db;
        }

        public AzureDbContext Db { get; }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.People.ToList());
        }


        [HttpGet("find/{id}/{password}")]
        public IActionResult Get([FromRoute] int id, string password)
        {
            var person = db.People.FirstOrDefault(w => w.PersonId == id);
            var person2 = db.People.FirstOrDefault(w => w.Password == password);
            if (person == null)
            {
                return NotFound();
            }
            else
                if (person2 == null) {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpGet("find2/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var person = db.People.FirstOrDefault(w => w.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost("add")]
        public IActionResult POST()
        {
            Person person = new Person
            {
                FirstName = Request.Form["firstName"],
                LastName = Request.Form["lastName"],
                Password = Request.Form["Password"],
                Age = Request.Form["Age"]
               
            };
            db.People.Add(person);
            db.SaveChanges();
            return Ok(person);
        }

        [HttpGet("delete/{test}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var person = db.People.FirstOrDefault(w => w.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }
            db.People.Remove(person);
            db.SaveChanges();
            return Ok("Deleted");
        }
        [HttpPut("update/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Person personObj)
        {
            var person = db.People.FirstOrDefault(w => w.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }


            try
            {
            person = personObj;
            person.PersonId = id;
            db.People.Update(person);
            db.SaveChanges();
            return Ok(person);
            }
            catch (Exception e)
            {
                LogException(e);
                return StatusCode(StatusCodes.Status500InternalServerError); ;
            }

        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] Person personObj)
        {
            var person = db.People.FirstOrDefault(w => w.PersonId == personObj.PersonId);
            if (person == null)
            {
                return NotFound();
            }


            try
            {
                person = personObj;
                db.People.Update(person);
                db.SaveChanges();
                return Ok(person);
            }
            catch (Exception e)
            {
                LogException(e);
                return StatusCode(StatusCodes.Status500InternalServerError); ;
            }


        }

        private void LogException(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
