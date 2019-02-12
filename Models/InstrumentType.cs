using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {

    [Table("instrumentTypes")]
    [DataContract]
    public partial class InstrumentType {
        public InstrumentType() {
        }

        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string model { get; set; }

        [DataMember]
        public string version { get; set; }

        [DataMember]
        public string manufacturer { get; set; }
    }
}
