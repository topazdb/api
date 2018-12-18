using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using api.db;

namespace api.Controllers{
    [Route("sets")]
    [ApiController]
    public class SetController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<Set>> Get() {
            using(var db = new TopazdbContext()) {
                return db.Sets.ToList();
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Set> Get(int id) {
            using(var db = new TopazdbContext()) {
                var setQuery = db.Sets.Where(a => a.id == id);
                
                if(setQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                
                return setQuery.First();
            }
        }

        [HttpGet("{name}")]
        public ActionResult<Set> Get(string name) {
            using(var db = new TopazdbContext()) {
                name = name.ToLower().Replace("-", " ");
                var setQuery = db.Sets.Where(a => a.name.ToLower() == name);

                if(setQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return setQuery.First();
            }
        }

        [HttpPost]
        public ActionResult<Set> Post(Set set) {
            using(var db = new TopazdbContext()) {
                if(!ModelState.IsValid) throw new HttpResponseException(HttpStatusCode.BadRequest);
                
                db.Sets.Add(set); 
                db.SaveChanges(); 
                return set;
            }
            
        }

        [HttpPut("{id}")]
        public string Put(int id, string name) {
            Delete(id); 
            Post(new Set() { name = name }); 
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public String Delete(int id) {
            using(var db = new TopazdbContext()) {
                var setQuery = db.Sets.Where(a => a.id == id);

                if(setQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                db.Sets.Remove(new Set() { id = id });
                db.SaveChanges();
                return "Deleted Successfully"; 
            }
        }
    }
}