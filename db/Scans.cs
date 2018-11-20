using System;
using System.Collections.Generic;

namespace api.db
{
    public partial class Scans {
        public Scans() {
            Lands = new HashSet<Lands>();
        }

        public long Id { get; set; }
        public long AuthorId { get; set; }
        public long SetId { get; set; }
        public long InstrumentId { get; set; }
        public long BarrelNo { get; set; }
        public long BulletNo { get; set; }
        public DateTime CreationDate { get; set; }
        public int? Magnification { get; set; }
        public int? Threshold { get; set; }
        public int? Resolution { get; set; }

        public Authors Author { get; set; }
        public Instruments Instrument { get; set; }
        public Sets Set { get; set; }
        public ICollection<Lands> Lands { get; set; }
    }
}
