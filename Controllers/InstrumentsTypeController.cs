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
    [Route("instrumenttype")]
    [ApiController]
    public class InstrumentsTypeController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<InstrumentType>> Get() {
            return Database.InstrumentTypes.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<InstrumentType> Get(int id) {
            var instrumentTypeQuery = Database.InstrumentTypes.Where(a => a.id == id);
            
            if(instrumentTypeQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return instrumentTypeQuery.First();
        }

        [HttpPost]
        public String Post(string model, string version, string manufacturer) {
            Database.InstrumentTypes.Add(new InstrumentType(){ model = model, version = version, manufacturer = manufacturer }); 
            Database.SaveChanges(); 
            return "Value Added Successfully"; 
        }

        [HttpPut("{id}")]
        public string Put(int id, string model, string version, string manufacturer) {
            Delete(id); 
            Post(model, version, manufacturer); 
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public String Delete(int id) {

            var instrumentTypeQuery = Database.InstrumentTypes.Where(a => a.id == id);

            if(instrumentTypeQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Database.InstrumentTypes.Remove(new InstrumentType() { id = id });
            Database.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}