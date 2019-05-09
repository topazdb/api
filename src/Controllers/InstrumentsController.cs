using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web.Http;
using api.db;
using api.Models;
using static api.Program;

namespace api.Controllers {

    [Route("instruments")]
    [ApiController]
    public class InstrumentsController : ControllerBase {
        private Context context;
        private Store store;
        private IQueryable<Instrument> instruments {
            get {
                return context.Instruments
                    .Include(i => i.type);
            }
        }

        public InstrumentsController(Context context, Store store) {
            this.context = context;
            this.store = store;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Instrument>> Get([FromUri] int limit = 100, [FromUri] int offset = 0) {
            return instruments
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        [HttpGet("{id:long}")]
        public ActionResult<Instrument> Get(long id) {
            var query = from i in instruments
                where i.id == id
                select i;
             
            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return query.First(); 
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Instrument> Post(Instrument instrument) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            context.Instruments.Add(instrument); 
            context.SaveChanges(); 
            return instrument;
        }

        [HttpPut("{id:long}")]
        [Authorize]
        public ActionResult<Instrument> Put(long id, Instrument updated) {
            var query = from i in instruments
                where i.id == id
                select i;

            if(!ModelState.IsValid || query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var instrument = query.First();
            instrument.merge(updated);

            context.Instruments.Update(instrument);
            context.SaveChanges();

            return instrument;
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public ActionResult<Instrument> Delete(long id) {
            var query = from i in instruments
                where i.id == id
                select i;
                
            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            var instrument = query.First();
            context.Instruments.Remove(instrument);
            context.SaveChanges();
            return instrument;
        }
    }
} 