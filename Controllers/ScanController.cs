using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;

namespace api.Controllers{
    [Route("scans")]
    [ApiController]
    public class ScanController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<Scan>> Get() {
            using(var db = new TopazdbContext()) {
                return db.Scans.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Scan> Get(int id) {
            using(var db = new TopazdbContext()) {
                var scanQuery = db.Scans.Where(a => a.id == id);
                
                if(scanQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                
                return scanQuery.First();
            }
        }

        [HttpPost]
        public String Post(int authorId, int setId, int instrumentId, int barrelNo, int bulletNo, DateTime creationDate, int magnification, int threshold, int resolution) {
            using(var db = new TopazdbContext()){
                db.Scans.Add(new Scan(){ authorId = authorId, setId = setId, instrumentId = instrumentId, barrelNo = barrelNo, bulletNo = bulletNo, creationDate = creationDate, magnification = magnification, threshold = threshold, resolution = resolution }); 
                db.SaveChanges(); 
            }
            return "Value Added Successfully"; 
        }

        [HttpPut("{id}")]
        public string Put(int id, int authorId, int setId, int instrumentId, int barrelNo, int bulletNo, DateTime creationDate, int magnification, int threshold, int resolution) {
            Delete(id); 
            Post(authorId, setId, instrumentId, barrelNo, bulletNo, creationDate, magnification, threshold, resolution); 
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public String Delete(int id) {
            using(var db = new TopazdbContext()) {
                var scanQuery = db.Scans.Where(a => a.id == id);

                if(scanQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                db.Scans.Remove(new Scan() { id = id });
                db.SaveChanges();
                return "Deleted Successfully"; 
            }
        }
    }
}