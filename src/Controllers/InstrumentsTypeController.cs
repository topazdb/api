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
    public class InstrumentsTypeController : ControllerBase {
        private Context context;

        public InstrumentsTypeController(Context context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstrumentType>> Get() {
            return context.InstrumentTypes.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<InstrumentType> Get(int id) {
            var instrumentTypeQuery = context.InstrumentTypes.Where(a => a.id == id);
            
            if(instrumentTypeQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return instrumentTypeQuery.First();
        }

        [HttpPost]
        public ActionResult<InstrumentType> Post(InstrumentType type) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            context.InstrumentTypes.Add(type); 
            context.SaveChanges(); 
            return type; 
        }

        [HttpPut("{id}")]
        public ActionResult<InstrumentType> Put(InstrumentType type) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Delete(type.id); 
            Post(type); 
            return type;
        }

        [HttpDelete("{id}")]
        public String Delete(long id) {

            var instrumentTypeQuery = context.InstrumentTypes.Where(a => a.id == id);

            if(instrumentTypeQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            context.InstrumentTypes.Remove(new InstrumentType() { id = id });
            context.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}