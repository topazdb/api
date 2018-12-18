using System;
using System.Collections.Generic;

namespace api.Models {
    public partial class Scan {
        public Scan() {
            lands = new HashSet<Land>();
        }

        public long id { get; set; }
        public long authorId { get; set; }
        public long setId { get; set; }
        public long instrumentId { get; set; }
        public long barrelNo { get; set; }
        public long bulletNo { get; set; }
        public DateTime creationDate { get; set; }
        public int? magnification { get; set; }
        public int? threshold { get; set; }
        public int? resolution { get; set; }

        public Author author { get; set; }
        public Instrument instrument { get; set; }
        public Set set { get; set; }
        public ICollection<Land> lands { get; set; }
    }
}
