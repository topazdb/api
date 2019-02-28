using System;
using System.Collections.Generic;

namespace api.Models {
    public partial class Land {
        public long id { get; set; }
        public long scanId { get; set; }
        public string path { get; set; }

        public Scan scan { get; set; }
    }
}
