using System;
using System.Collections.Generic;

namespace api.db {
    public partial class Author {
        public Author() {
            scans = new HashSet<Scan>();
        }

        public long id { get; set; }
        public string name { get; set; }
        public string contact { get; set; }

        public ICollection<Scan> scans { get; set; }
    }
}
