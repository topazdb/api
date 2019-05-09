using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Web.Http;
using api.db;
using api.Models;
using static api.Program;

namespace api.Controllers{
    [Route("instrument-types")]
    [ApiController]
    public class InstrumentTypeController : ControllerBase {
        private Context context;
        private Store store;

        public InstrumentTypeController(Context context, Store store) {
            this.context = context;
            this.store = store;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstrumentType>> Get([FromUri] int limit = 100, [FromUri] int offset = 0) {
            return context.InstrumentTypes
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        [HttpGet("{id:long}")]
        public ActionResult<InstrumentType> Get(long id) {
            var query = from type in context.InstrumentTypes
                where type.id == id
                select type;
            
            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return query.First();
        }

        [HttpPost]
        [Authorize]
        public ActionResult<InstrumentType> Post(InstrumentType type) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            context.InstrumentTypes.Add(type); 
            context.SaveChanges(); 
            return type; 
        }

        [HttpPut("{id:long}")]
        [Authorize]
        public ActionResult<InstrumentType> Put(long id, InstrumentType updated) {
            var query = from t in context.InstrumentTypes
                where t.id == id
                select t;

            if(!ModelState.IsValid || query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            var type = query.First();
            type.merge(updated);
            
            context.InstrumentTypes.Update(type);
            context.SaveChanges();
            
            return type;
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public ActionResult<InstrumentType> Delete(long id) {
            var query = from t in context.InstrumentTypes
                where t.id == id
                select t;

            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var type = query.First();
            context.InstrumentTypes.Remove(type);
            context.SaveChanges();
            return type;
        }
    }
}