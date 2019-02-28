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
    [Route("instruments")]
    [ApiController]
    public class InstrumentsController : ControllerBase {
        private Context context;

        public InstrumentsController(Context context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Instrument>> Get() {
            return context.Instruments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Instrument> Get(int id) {
            var instrumentQuery = context.Instruments.Where(a => a.id == id);
            
            if(instrumentQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return instrumentQuery.First(); 
        }

        [HttpPost]
        public ActionResult<Instrument> Post(Instrument instrument) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            context.Instruments.Add(instrument); 
            context.SaveChanges(); 
            
            return instrument;
        }

        [HttpPut("{id}")]
        public string Put(Instrument instrument) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Delete(instrument.id); 
            Post(instrument); 
            return "Value updated Successfully";
        }

        [HttpDelete("{id}")]
        public void Delete(long id) {
            var instrumentQuery = context.Instruments.Where(a => a.id == id);

            if(instrumentQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            context.Instruments.Remove(new Instrument() { id = id });
            context.SaveChanges();
        }
    }
} 