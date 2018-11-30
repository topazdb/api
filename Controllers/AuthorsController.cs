﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;

namespace api.Controllers {
    [Route("authors")]
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
        public String Post(string name, string contact) {
            if(name == null){
                throw new Exception("Name cannot be a null value");
            }
            using(var db = new TopazdbContext()){
                db.Authors.Add(new Author(){ name = name, contact = contact }); 
                db.SaveChanges(); 
            }
            return "Value Added Successfully"; 
        }

        [HttpPut("{id}")]
        public string Put(int id, string name, string contact) {
            if(name == null){
                throw new Exception("Name cannot be a null value");
            }
            Delete(id); 
            Post(name, contact); 
            return "Value updated Successfully"; 
        }

        [HttpDelete("{id}")]
        public String Delete(int id) {
            using(var db = new TopazdbContext()) {
                var authorQuery = db.Authors.Where(a => a.id == id);

                if(authorQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                db.Authors.Remove(new Author() { id = id });
                db.SaveChanges();
                return "Deleted Successfully"; 
            }
        }
    }
}
