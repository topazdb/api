using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;
using api.Models;
using static api.Program;

namespace api.Controllers{
    [Route("scans")]
    [ApiController]
    public class ScanController : ControllerBase {
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
        public ActionResult<Scan> Post(Scan scan) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Database.Scans.Add(scan); 
            Database.SaveChanges(); 
            return scan;
        }

        [HttpPut("{id}")]
        public ActionResult<Scan> Put(Scan scan) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Delete(scan.id); 
            Post(scan); 
            return scan;
        }

        [HttpDelete("{id}")]
        public String Delete(long id) {
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