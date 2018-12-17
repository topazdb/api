using System;
using System.Collections.Generic;

namespace api.db {
    public partial class Set {
        public Set() {
            scans = new HashSet<Scan>();
            creationDate = DateTime.Now;
        }

        public long id { get; set; }
        public string name { get; set; }
        public DateTimeOffset creationDate { get; set; }
        public ICollection<Scan> scans { get; set; }
    }
}
