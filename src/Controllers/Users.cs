using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using api.Models;
using api.Models.Login;

namespace api.Controllers {

    [ApiController]
    [Route("users")]
    public class Users : ControllerBase {
        private static readonly HttpClient HttpClient = new HttpClient();
        private static readonly string oktaDomain = Environment.GetEnvironmentVariable("TOPAZ_OKTA_DOMAIN");
        private static readonly string oktaAuthServ = Environment.GetEnvironmentVariable("TOPAZ_OKTA_AUTHSERV") ?? "default";
        private static readonly string oktaTokenUrl = oktaDomain + "/oauth2/" + oktaAuthServ + "/v1/token";

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestPayload payload) {

            try {
                System.Console.WriteLine(payload.ToString());
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, oktaTokenUrl);
                request.Content = new StringContent(payload.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await HttpClient.SendAsync(request);
                string responseString = await response.Content.ReadAsStringAsync();
                
                if(response.StatusCode != HttpStatusCode.OK) {
                    throw new Exception(responseString);
                }

                LoginResponsePayload outgoing = JsonConvert.DeserializeObject<LoginResponsePayload>(responseString);
                return Ok(outgoing);

            } catch(Exception e) {
                return BadRequest(e);

            }
        }
    }
}