using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {
    [Table("instruments")]
    [DataContract]
    public partial class Instrument : Model<Instrument> {
        public Instrument() {
            scans = new HashSet<Scan>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long id { get; set; }

        [UserEditable]
        public virtual long instrumentTypeId { get; set; }

        [DataMember]
        [UserEditable]
        public virtual string serialNo { get; set; }

        [DataMember]
        [UserEditable]
        public virtual DateTimeOffset calibrationDate { get; set; }

        [ForeignKey("instrumentTypeId")]
        [DataMember]
        public virtual InstrumentType type { get; set; }
        
        public virtual ICollection<Scan> scans { get; set; }
    }
}
