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
    [Route("scans")]
    [ApiController]
    public class ScanController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<Scan>> Get() {
            return Database.Scans.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Scan> Get(int id) {
            var scanQuery = Database.Scans.Where(a => a.id == id);
            
            if(scanQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return scanQuery.First();
        }

        [HttpPost]
        public String Post(int authorId, int setId, int instrumentId, int barrelNo, int bulletNo, int magnification, int threshold, int resolution) {
            Database.Scans.Add(new Scan() { 
                authorId = authorId, 
                setId = setId, 
                instrumentId = instrumentId, 
                barrelNo = barrelNo, 
                bulletNo = bulletNo, 
                magnification = magnification, 
                threshold = threshold, 
                resolution = resolution 
            }); 
                
            Database.SaveChanges(); 
            return "Value Added Successfully"; 
        }

        [HttpPut("{id}")]
        public string Put(int id, int authorId, int setId, int instrumentId, int barrelNo, int bulletNo, int magnification, int threshold, int resolution) {
            Delete(id); 
            Post(authorId, setId, instrumentId, barrelNo, bulletNo, magnification, threshold, resolution); 
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public String Delete(int id) {
            var scanQuery = Database.Scans.Where(a => a.id == id);

            if(scanQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Database.Scans.Remove(new Scan() { id = id });
            Database.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}