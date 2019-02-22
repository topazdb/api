using System;

namespace api.db {
    class CredentialProvider {
        static public string hostname {
            get {
                return Environment.GetEnvironmentVariable("TOPAZ_HOST");
            }
        }

        static public string user {
            get {
                return Environment.GetEnvironmentVariable("TOPAZ_USER");
            }
        }

        static public string password {
            get {
                return Environment.GetEnvironmentVariable("TOPAZ_PASSWORD");
            }
        }

        static public string database {
            get {
                return Environment.GetEnvironmentVariable("TOPAZ_DATABASE");
            }
        }
    }
}