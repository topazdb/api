using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using api.db;
using api.Models;
using static api.Program;
using static api.Util.URL;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace api.Controllers {

    [Route("sets")]
    [ApiController]
    public class SetController : ControllerBase {

        private Context context;
        private Store store;

        public IQueryable<Set> sets {
            get {
                return context.Sets
                    .Include(s => s.scans)
                    .Include("scans.author")
                    .Include("scans.instrument")
                    .Include("scans.instrument.type")
                    .Include("scans.lands")
                    .Include(s => s.subsets);
            }
        }

        public SetController(Context context, Store store) {
            this.context = context;
            this.store = store;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Set>> Get([FromUri] int limit = 100, [FromUri] int offset = 0) {
            return (from set in sets
                    where set.parentId == null
                    select set)
            .Skip(offset)
            .Take(limit)
            .ToList();
        }

        [HttpGet("{id:long}")]
        public ActionResult<Set> Get(long id) {
            Set result = GetParentsRecursive(id);
            
            if(result == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return result;
        }
        
        [HttpGet("{id:long}/scans")]
        public ActionResult<IEnumerable<Scan>> GetScans(long id) {          
            var collection = context.Scans
                .Include(s => s.author)
                .Include(s => s.set)
                .Include(s => s.instrument)
                .Include(s => s.instrument.type)
                .Include(s => s.lands);

            var query = from scan in collection
                where scan.set.id == id
                select scan;
            
            return query.ToList();
        }

        private Set GetParentsRecursive(long id) {
            var query = from set in sets
                where set.id == id
                select set;
            
            if(query.Count() == 0) return null;
            
            Set child = query.First();
            if(child.parentId != null) {
                child.parent = GetParentsRecursive((long) child.parentId);
            }

            return child;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Set> Post([FromBody] JObject json) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            Set s = json["set"].ToObject<Set>();
            ICollection<Scan> scans = json["scans"].ToObject<HashSet<Scan>>();
            
            Set set = new Set();
            set.name = s.name;
            set.scans = scans;

            context.Sets.Add(set); 
            context.SaveChanges(); 
            return set;            
        }

        [HttpPut("{id:long}")]
        [Authorize]
        public ActionResult<Set> Put(long id, Set updated) {
            var query = from s in sets
                where s.id == id
                select s;

            if(!ModelState.IsValid || query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var set = query.First();
            set.merge(updated);
            context.Sets.Update(set);
            return set;
        }

        [HttpDelete("{id:long}")]
        [Authorize]
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
