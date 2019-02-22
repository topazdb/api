using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using api.db;
using api.Models;
using static api.Program;
using static api.Util.URL;

namespace api.Controllers {

    [Route("sets")]
    [ApiController]
    public class SetController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<SetView>> Get() {
            return Database.SetView.ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult<SetView> Get(int id) {
            var setQuery = Database.SetView.Where(a => a.id == id);
            
            if(setQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return setQuery.First();
        }

        [HttpGet("{name}")]
        public ActionResult<SetView> Get(string name) {
            name = decode(name);
            var setQuery = Database.SetView.Where(a => a.name.ToLower() == name);

            if(setQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return setQuery.First();
        }

        [HttpGet("{name}/scans")]
        public ActionResult<Scan> GetScans(string name) {
            name = decode(name);

            var query = from scan in Database.Scans 
                join set in Database.Sets on scan.set equals set
                where set.name == name select scan;

            System.Console.WriteLine(query.Count());
            
            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return query.First();
        }

        [HttpPost]
        public ActionResult<Set> Post(Set set) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            Database.Sets.Add(set); 
            Database.SaveChanges(); 
            return set;            
        }

        [HttpPut("{id}")]
        public string Put(Set set) {
            Delete(set.id); 
            Post(set);
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public String Delete(long id) {
            var setQuery = Database.Sets.Where(a => a.id == id);

            if(setQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Database.Sets.Remove(new Set() { id = id });
            Database.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}