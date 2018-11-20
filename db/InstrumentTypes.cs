using System;
using System.Collections.Generic;

namespace api.db {
    public partial class InstrumentTypes {
        public InstrumentTypes() {
            Instruments = new HashSet<Instruments>();
        }

        public long Id { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string Manufacturer { get; set; }

        public ICollection<Instruments> Instruments { get; set; }
    }
}
