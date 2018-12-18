using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;
using static api.Program;

namespace api.Controllers{
    [Route("lands")]
    [ApiController]
    public class LandController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<Land>> Get() {
            return Database.Lands.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Land> Get(int id) {
            var landQuery = Database.Lands.Where(a => a.id == id);
            
            if(landQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return landQuery.First();
        }

        [HttpPost]
        public String Post(int scanId, string path) {
            Database.Lands.Add(new Land(){ scanId = scanId, path = path }); 
            Database.SaveChanges(); 
            
            return "Value Added Successfully"; 
        }

        [HttpPut("{id}")]
        public string Put(int id, int scanId, string path) {
            Delete(id); 
            Post(scanId, path); 
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public String Delete(int id) {
            var landQuery = Database.Lands.Where(a => a.id == id);

            if(landQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Database.Lands.Remove(new Land() { id = id });
            Database.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}