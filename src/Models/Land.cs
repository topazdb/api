using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    [Table("lands")]
    [DataContract]
    public partial class Land {
        [DataMember]
        public long id { get; set; }

        [DataMember]
        public long scanId { get; set; }

        [DataMember]
        public string path { get; set; }

        public Scan scan { get; set; }
    }
}
