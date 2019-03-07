using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {
    [Table("instruments")]
    [DataContract]
    public partial class Instrument : Model {
        public Instrument() {
            scans = new HashSet<Scan>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [UserEditable]
        public long instrumentTypeId { get; set; }

        [DataMember]
        [UserEditable]
        public string serialNo { get; set; }

        [DataMember]
        [UserEditable]
        public DateTimeOffset calibrationDate { get; set; }

        [ForeignKey("instrumentTypeId")]
        [DataMember]
        public InstrumentType type { get; set; }
        
        public ICollection<Scan> scans { get; set; }
    }
}
