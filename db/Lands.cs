using System;
using System.Collections.Generic;

namespace api.db {
    public partial class Lands {
        public long Id { get; set; }
        public long ScanId { get; set; }
        public string Path { get; set; }

        public Scans Scan { get; set; }
    }
}
