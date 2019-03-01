using System.Text.RegularExpressions;

namespace api.Util {
    public class URL {
        public static string decode(string value) {
            value = value.ToLower();
            value = value.Replace("-", " ");
            value = value.Replace("%2d", "-");
            value = value.Replace("%2D", "-");
            
            return value;
        }

        public static string encode(string value) {
            value = value.ToLower();
            value = value.Replace("-", "%2D");
            value = value.Replace(" ", "-");

            return value;
        }
    }
}