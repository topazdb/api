using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace api.db {

    [Serializable()]
    public partial class Authors {
        public Authors() {
            Scans = new HashSet<Scans>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }

        public ICollection<Scans> Scans { get; set; }
    }
}
