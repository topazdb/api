using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    [Table("instruments")]
    [DataContract]
    public partial class Instrument {
        public Instrument() {
            scans = new HashSet<Scan>();
        }

        [DataMember]
        public long id { get; set; }

        [DataMember]
        public long instrumentTypeId { get; set; }

        [DataMember]
        public string serialNo { get; set; }

        [DataMember]
        public DateTimeOffset calibrationDate { get; set; }

        [ForeignKey("instrumentTypeId")]
        [DataMember]
        public InstrumentType instrumentType { get; set; }
        
        public ICollection<Scan> scans { get; set; }
    }
}
