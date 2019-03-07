using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using api.db;
using api.Models;
using static api.Program;

namespace api.Controllers{
    [Route("lands")]
    [ApiController]
    public class LandController : ControllerBase {
        private Context context;

        public LandController(Context context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Land>> Get() {
            return context.Lands.ToList();
        }

        [HttpGet("{id:long}")]
        public FileStreamResult Get(long id) {
            var landQuery = from l in context.Lands 
                where l.id == id 
                select l;

            if(landQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Land land = landQuery.First();
            FileStream stream = new FileStream(land.path, FileMode.Open, FileAccess.Read);
            return File(stream, "application/octet-stream", Path.GetFileName(land.path));
        }

        [HttpPost]
        public String Post(Land land) {
            context.Lands.Add(land); 
            context.SaveChanges(); 
            
            return "Value Added Successfully"; 
        }

        [HttpPut("{id}")]
        public string Put(Land land) {
            Delete(land.id); 
            Post(land); 
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public String Delete(long id) {
            var landQuery = context.Lands.Where(a => a.id == id);

            if(landQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            context.Lands.Remove(new Land() { id = id });
            context.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}