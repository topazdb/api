using System;

namespace api.Models {
    public partial class SetView {
        public long id { get; set; }
        public string name { get; set; }
        public DateTimeOffset creationDate { get; set; }
        public long barrelCount { get; set; }
        public long bulletCount { get; set; }
        public DateTimeOffset? lastScanDate { get; set; }
    }
}