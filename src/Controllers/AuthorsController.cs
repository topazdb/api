using System;
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

namespace api.Controllers {

    [Route("authors")]
    [ApiController]
    public class AuthorsController : ControllerBase {
        private Context context;
        private Store store;
        
        public AuthorsController(Context context, Store store) {
            this.context = context;
            this.store = store;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get([FromUri] int limit = 100, [FromUri] int offset = 0) {
            return context.Authors
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        [HttpGet("{id:long}")]
        public ActionResult<Author> Get(long id) {
            var query = from a in context.Authors
                where a.id == id
                select a;

            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return query.First();
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Author> Post(Author author) {
            if(!ModelState.IsValid) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            context.Authors.Add(author); 
            context.SaveChanges(); 
            return author;
        }

        [HttpPut("{id:long}")]
        [Authorize]
        public ActionResult<Author> Put(long id, Author updated) {
            var query = from a in context.Authors
                where a.id == id
                select a;

            if(!ModelState.IsValid || query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            
            var author = query.First();
            author.merge(updated);

            context.Authors.Update(author);
            context.SaveChanges();
            return author;
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        public ActionResult<Author> Delete(long id) {
            var query = from a in context.Authors
                where a.id == id
                select a;

            if(query.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            var author = query.First();
            context.Authors.Remove(author);
            context.SaveChanges();
            return author;
        }
    }
}
