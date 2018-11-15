namespace db {
    class DatabaseCredentialProvider {
        public string hostname {
            get {
                return Environment.GetEnvironmentVariable("TOPAZ_HOST");
            }
        }

        public string user {
            get {
                return Environment.GetEnvironmentVariable("TOPAZ_USER");
            }
        }

        public string password {
            get {
                return Environment.GetEnvironmentVariable("TOPAZ_PASSWORD");
            }
        }

        public string database {
            get {
                return Environment.GetEnvironmentVariable("TOPAZ_DATABASE");
            }
        }
    }
}