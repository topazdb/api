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

namespace api.Controllers{
    [Route("settings")]
    [ApiController]
    public class SettingsController : ControllerBase{
        [HttpGet]
        public ActionResult<IEnumerable<Setting>> Get() {
            return Database.Settings.ToList();
        }

        [HttpPost]
        public String Post(string name, string value) {
            Database.Settings.Add(new Setting(){ name = name, value = value }); 
            Database.SaveChanges();
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
            var settingQuery = Database.Settings.Where(a => a.name == name);

            if(settingQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Database.Settings.Remove(new Setting() { name = name });
            Database.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}