using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;
using static api.Program;

namespace api.Controllers{
    [Route("instruments")]
    [ApiController]
    public class InstrumentsController : ControllerBase {
        [HttpGet]
        public ActionResult<IEnumerable<Instrument>> Get() {
            return Database.Instruments.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Instrument> Get(int id) {
            var instrumentQuery = Database.Instruments.Where(a => a.id == id);
            
            if(instrumentQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return instrumentQuery.First(); 
        }

        [HttpPost]
        public string Post(int instrumentTypeId, string serialNo, DateTimeOffset calibrationDate, InstrumentType instrumentType) {
            Database.Instruments.Add(new Instrument(){ instrumentTypeId = instrumentTypeId, serialNo = serialNo, calibrationDate = calibrationDate, instrumentType = instrumentType }); 
            Database.SaveChanges(); 
            
            return "Value Added Successfully"; 
        }

        [HttpPut("{id}")]
        public string Put(int id, int instrumentTypeId, string serialNo, DateTimeOffset calibrationDate, InstrumentType instrumentType) {
            Delete(id); 
            Post(instrumentTypeId, serialNo, calibrationDate, instrumentType); 
            return "Value updated Successfully";
        }

        [HttpDelete("{id}")]
        public void Delete(int id) {
            var instrumentQuery = Database.Instruments.Where(a => a.id == id);

            if(instrumentQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Database.Instruments.Remove(new Instrument() { id = id });
            Database.SaveChanges();
        }
    }
} 