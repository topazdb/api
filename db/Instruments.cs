using System;
using System.Collections.Generic;

namespace api.db {
    public partial class Instruments {
        public Instruments() {
            Scans = new HashSet<Scans>();
        }

        public long Id { get; set; }
        public long InstrumentTypeId { get; set; }
        public string SerialNo { get; set; }
        public DateTimeOffset CalibrationDate { get; set; }

        public InstrumentTypes InstrumentType { get; set; }
        public ICollection<Scans> Scans { get; set; }
    }
}
