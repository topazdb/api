using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {

    [Table("instrumentTypes")]
    [DataContract]
    public partial class InstrumentType : Model {
        public InstrumentType() {
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [DataMember]
        [UserEditable]
        public string model { get; set; }

        [DataMember]
        [UserEditable]
        public int version { get; set; }

        [DataMember]
        [UserEditable]
        public string manufacturer { get; set; }
    }
}
