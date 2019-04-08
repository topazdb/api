using System;

namespace api.Models.Login {
    public class LoginResponsePayload {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string id_token { get; set; }
    }
}