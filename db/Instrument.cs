using System;
using System.Collections.Generic;

namespace api.db {
    public partial class Instrument {
        public Instrument() {
            scans = new HashSet<Scan>();
        }

        public long id { get; set; }
        public long instrumentTypeId { get; set; }
        public string serialNo { get; set; }
        public DateTimeOffset calibrationDate { get; set; }

        public InstrumentType instrumentType { get; set; }
        public ICollection<Scan> scans { get; set; }
    }
}
