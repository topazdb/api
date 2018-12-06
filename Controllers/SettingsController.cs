using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;

namespace api.Controllers{
    [Route("settings")]
    [ApiController]
    public class SettingsController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<Setting>> Get() {
            using(var db = new TopazdbContext()) {
                return db.Settings.ToList();
            }
        }

        [HttpPost]
        public String Post(string name, string value) {
            using(var db = new TopazdbContext()){
                db.Settings.Add(new Setting(){ name = name, value = value }); 
                db.SaveChanges(); 
            }
            return "Value Added Successfully"; 
        }

        [HttpPut("{name}")]
        public string Put(string name, string value) {
            Delete(name); 
            Post(name, value); 
            return "Value updated Successfully"; 
        }

        [HttpDelete("{name}")]
        public String Delete(string name) {
            using(var db = new TopazdbContext()) {
                var settingQuery = db.Settings.Where(a => a.name == name);

                if(settingQuery.Count() == 0) {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                db.Settings.Remove(new Setting() { name = name });
                db.SaveChanges();
                return "Deleted Successfully"; 
            }
        }
    }
}