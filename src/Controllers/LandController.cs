using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using api.db;
using api.Models;
using static api.Program;

namespace api.Controllers{
    [Route("lands")]
    [ApiController]
    public class LandController : ControllerBase {
        private Context context;
        private Store store;

        public LandController(Context context, Store store) {
            this.context = context;
            this.store = store;
        }

        [HttpGet("{id:long}")]
        public FileStreamResult Get(long id) {
            var landQuery = from l in context.Lands 
                where l.id == id 
                select l;

            if(landQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Land land = landQuery.First();
            FileStream stream = new FileStream(land.path, FileMode.Open, FileAccess.Read);
            return File(stream, "application/octet-stream", Path.GetFileName(land.path));
        }


        [HttpDelete("{id:long}")]
        [Authorize]
        public ActionResult<Land> Delete(long id) {
            var landQuery = from l in context.Lands
                where l.id == id
                select l;

            if(landQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var land = landQuery.First();
            context.Lands.Remove(land);
            context.SaveChanges();
            return land;
        }
    }
}