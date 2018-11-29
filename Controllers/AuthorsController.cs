using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;

namespace api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase {
        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get() {
            using(var db = new TopazdbContext()) {
                return db.Authors.ToList();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Author> Get(int id) {
            using(var db = new TopazdbContext()) {
                var authorQuery = db.Authors.Where(a => a.id == id);
                
                if(authorQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return authorQuery.First();
            }
        }

        [HttpPost]
        public void Post([FromBody] int id, string name, string contact) {
            if(name == null){
                throw new Exception("Name cannot be a null value");
            }
            using(var db = new TopazdbContext()){
                db.Authors.Add(new Author(){ id = id, name = name, contact = contact }); 
                db.SaveChanges(); 
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string name, string contact) {
            if(name == null){
                throw new Exception("Name cannot be a null value");
            }
           Delete(id); 
           Post(id, name, contact); 
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            using(var db = new TopazdbContext()) {
                var authorQuery = db.Authors.Where(a => a.id == id);

                if(authorQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                db.Authors.Remove(new Author() { id = id });
                db.SaveChanges();
            }
        }
    }
}
