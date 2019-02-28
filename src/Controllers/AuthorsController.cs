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

namespace api.Controllers {

    [Route("authors")]
    [ApiController]
    public class AuthorsController : ControllerBase {
        private Context context;
        
        public AuthorsController(Context context) {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get() {
            return context.Authors.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Author> Get(int id) {
            var authorQuery = context.Authors.Where(a => a.id == id);
            
            if(authorQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return authorQuery.First();
        }

        [HttpPost]
        public ActionResult<Author> Post(Author author) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            context.Authors.Add(author); 
            context.SaveChanges(); 
            return author;
        }

        [HttpPut("{id}")]
        public ActionResult<Author> Put(Author author) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            Delete(author.id); 
            Post(author); 
            return author;
        }

        [HttpDelete("{id}")]
        public String Delete(long id) {
            var authorQuery = context.Authors.Where(a => a.id == id);

            if(authorQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            context.Authors.Remove(new Author() { id = id });
            context.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}
