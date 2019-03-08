using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {
    [Table("lands")]
    [DataContract]
    public partial class Land : Model<Land> {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long id { get; set; }

        [DataMember]
        [UserEditable]
        public virtual long scanId { get; set; }

        [UserEditable]
        public virtual string path { get; set; }

        public virtual Scan scan { get; set; }
    }
}
