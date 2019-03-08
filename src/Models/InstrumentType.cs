using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {

    [Table("instrumentTypes")]
    [DataContract]
    public partial class InstrumentType : Model<InstrumentType> {
        public InstrumentType() {
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long id { get; set; }

        [DataMember]
        [UserEditable]
        public virtual string model { get; set; }

        [DataMember]
        [UserEditable]
        public virtual int version { get; set; }

        [DataMember]
        [UserEditable]
        public virtual string manufacturer { get; set; }
    }
}
