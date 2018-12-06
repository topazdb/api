using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;

namespace api.Controllers{
    [Route("instrumenttype")]
    [ApiController]
    public class InstrumentsTypeController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<InstrumentType>> Get() {
            using(var db = new TopazdbContext()) {
                return db.InstrumentTypes.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<InstrumentType> Get(int id) {
            using(var db = new TopazdbContext()) {
                var instrumentTypeQuery = db.InstrumentTypes.Where(a => a.id == id);
                
                if(instrumentTypeQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                
                return instrumentTypeQuery.First();
            }
        }

        [HttpPost]
        public String Post(string model, string version, string manufacturer) {
            using(var db = new TopazdbContext()){
                db.InstrumentTypes.Add(new InstrumentType(){ model = model, version = version, manufacturer = manufacturer }); 
                db.SaveChanges(); 
            }
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
            using(var db = new TopazdbContext()) {
                var instrumentTypeQuery = db.InstrumentTypes.Where(a => a.id == id);

                if(instrumentTypeQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                db.InstrumentTypes.Remove(new InstrumentType() { id = id });
                db.SaveChanges();
                return "Deleted Successfully"; 
            }
        }
    }
}