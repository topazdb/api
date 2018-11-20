using System;
using System.Collections.Generic;

namespace api.db {
    public partial class InstrumentType {
        public InstrumentType() {
            instruments = new HashSet<Instrument>();
        }

        public long id { get; set; }
        public string model { get; set; }
        public string version { get; set; }
        public string manufacturer { get; set; }

        public ICollection<Instrument> instruments { get; set; }
    }
}
