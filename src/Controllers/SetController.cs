using System;
using System.Collections.Generic;
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

        public SetController(Context context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SetView>> Get() {
            return context.SetView.ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult<SetView> Get(int id) {
            var setQuery = context.SetView.Where(a => a.id == id);
            
            if(setQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return setQuery.First();
        }

        [HttpGet("{name}")]
        public ActionResult<SetView> Get(string name) {
            name = decode(name);
            var setQuery = context.SetView.Where(a => a.name.ToLower() == name);

            if(setQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return setQuery.First();
        }

        [HttpGet("{name}/scans")]
        public ActionResult<Scan> GetScans(string name) {
            name = decode(name);

            var query = from scan in context.Scans 
                join set in context.Sets on scan.set equals set
                where set.name == name select scan;

            System.Console.WriteLine(query.Count());
            
            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return query.First();
        }

        [HttpGet("{id}/barrels")]
        public ActionResult<Dictionary<long?, List<long>>> GetBarrels(long id) {
            var query = from scan in context.Scans
                where scan.set.id == id
                group scan by scan.barrelNo into barrels
                select barrels;

            return query.ToDictionary(
                scan => (scan.Key == null) ? 0 : scan.Key, 
                scan => (from barrel in scan orderby barrel.bulletNo select barrel.bulletNo).ToList()
            );
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

        [HttpPut("{id}")]
        public string Put(Set set) {
            Delete(set.id); 
            Post(set);
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public string Delete(long id) {
            var setQuery = context.Sets.Where(a => a.id == id);

            if(setQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            context.Sets.Remove(new Set() { id = id });
            context.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}