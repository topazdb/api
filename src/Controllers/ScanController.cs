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
        private Context context;
        
        public ScanController(Context context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Scan>> Get() {
            return context.Scans.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Scan> Get(int id) {
            var scanQuery = context.Scans.Where(a => a.id == id);
            
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

            context.Scans.Add(scan); 
            context.SaveChanges(); 
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
            var scanQuery = context.Scans.Where(a => a.id == id);

            if(scanQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            context.Scans.Remove(new Scan() { id = id });
            context.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}