using System;
using System.Collections.Generic;

namespace api.db {
    public partial class Sets {
        public Sets() {
            Scans = new HashSet<Scans>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreationDate { get; set; }

        public ICollection<Scans> Scans { get; set; }
    }
}
