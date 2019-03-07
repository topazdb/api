using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using api.db;
using api.Models;
using static api.Program;
using static api.Util.URL;

namespace api.Controllers {

    [Route("sets")]
    [ApiController]
    public class SetController : ControllerBase {

        private Context context;

        public IQueryable<Set> sets {
            get {
                return context.Sets
                    .Include(s => s.scans);
            }
        }

        public SetController(Context context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Set>> Get([FromUri] int limit = 100, [FromUri] int offset = 0) {
            return sets
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        [HttpGet("{id:long}")]
        public ActionResult<Set> Get(long id) {
            var query = from set in sets
                where set.id == id
                select set;
            
            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return query.First();
        }

        [HttpGet("{id:long}/rundown")]
        public ActionResult<Dictionary<long?, List<long>>> GetRundown(long id) {
            var query = from scan in context.Scans
                where scan.set.id == id
                group scan by scan.barrelNo into barrels
                select barrels;

            return query.ToDictionary( // barrel: [bullets...]
                scan => (scan.Key == null) ? 0 : scan.Key, 
                scan => (from barrel in scan orderby barrel.bulletNo select barrel.bulletNo).Distinct().ToList()
            );
        }

        [HttpGet("{id:long}/{barrel:long}/{bullet:long}")]
        public ActionResult<IEnumerable<Scan>> GetScans(long id, long? barrel, long bullet) {
            barrel = barrel == 0 ? null : barrel;
            
            var collection = context.Scans
                .Include(s => s.author)
                .Include(s => s.set)
                .Include(s => s.instrument)
                .Include(s => s.instrument.type)
                .Include(s => s.lands);

            var query = from scan in collection
                where scan.set.id == id && scan.barrelNo == barrel && scan.bulletNo == bullet
                select scan;
            
            return query.ToList();
        }

        [HttpPost]
        public ActionResult<Set> Post(Set set) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            context.Sets.Add(set); 
            context.SaveChanges(); 
            return set;            
        }

        [HttpPut("{id:long}")]
        public ActionResult<Set> Put(long id, Set updated) {
            var query = from s in sets
                where s.id == id
                select s;

            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var set = query.First();
            set.name = updated.name;
            context.Sets.Update(set);
            return set;
        }

        [HttpDelete("{id:long}")]
        public ActionResult<Set> Delete(long id) {
            var query = from s in sets
                where s.id == id
                select s;

            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var set = query.First();
            context.Sets.Remove(set);
            context.SaveChanges();
            return set;
        }
    }
}