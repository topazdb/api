using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using api.db;
using api.Models;

namespace api {
    [Route("status")]
    [ApiController]
    public class StatusController : ControllerBase {
        private Context context;
        private Store store;

        public StatusController(Context context, Store store) {
            this.context = context;
            this.store = store;
        }

        [HttpGet("populator")]
        public ActionResult<PopulatorStatus> GetPopulator() {
            return store.populator.status;
        }

        [HttpGet("populator/errors")]
        public ActionResult<Dictionary<string, List<string>>> GetPopulatorErrors() {
            return store.populator.status.errors;
        }
    }
}