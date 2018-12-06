using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;

namespace api.Controllers{
    [Route("lands")]
    [ApiController]
    public class LandController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<Land>> Get() {
            using(var db = new TopazdbContext()) {
                return db.Lands.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Land> Get(int id) {
            using(var db = new TopazdbContext()) {
                var landQuery = db.Lands.Where(a => a.id == id);
                
                if(landQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                
                return landQuery.First();
            }
        }

        [HttpPost]
        public String Post(int scanId, string path) {
            using(var db = new TopazdbContext()){
                db.Lands.Add(new Land(){ scanId = scanId, path = path }); 
                db.SaveChanges(); 
            }
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
            using(var db = new TopazdbContext()) {
                var landQuery = db.Lands.Where(a => a.id == id);

                if(landQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                db.Lands.Remove(new Land() { id = id });
                db.SaveChanges();
                return "Deleted Successfully"; 
            }
        }
    }
}