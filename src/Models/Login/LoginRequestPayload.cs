using System;

namespace api.Models.Login {
    public class LoginRequestPayload {

        // properties set by the client
        public string code { get; set; }
        public string state { get; set; }
        public string redirect_uri { get; set; }
        public string scope { get; set; } = "openid profile email";

        // properties set by the api
        public readonly string client_id = Environment.GetEnvironmentVariable("TOPAZ_OKTA_CLIENTID");
        public readonly string client_secret = Environment.GetEnvironmentVariable("TOPAZ_OKTA_CLIENTSECRET");
        public readonly string grant_type = "authorization_code";
        
        public string ToString() {
            string result = "";
            result += $"code={code}";
            result += $"&state={state}";
            result += $"&redirect_uri={redirect_uri}";
            result += $"&scope={scope}";
            result += $"&client_id={client_id}";
            result += $"&client_secret={client_secret}";
            result += $"&grant_type={grant_type}";
            return result;
        }
    }
}