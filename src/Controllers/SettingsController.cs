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
    [Route("settings")]
    [ApiController]
    public class SettingsController : ControllerBase {
        private Context context;
        private Store store;

        public SettingsController(Context context, Store store) {
            this.context = context;
            this.store = store;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Setting>> Get() {
            return context.Settings.ToList();
        }

        [HttpPost]
        public String Post(string name, string value) {
            context.Settings.Add(new Setting(){ name = name, value = value }); 
            context.SaveChanges();
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
            var settingQuery = context.Settings.Where(a => a.name == name);

            if(settingQuery.Count() == 0) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            context.Settings.Remove(new Setting() { name = name });
            context.SaveChanges();
            return "Deleted Successfully"; 
        }
    }
}