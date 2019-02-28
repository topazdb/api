using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    [Table("setView")]
    [DataContract]
    public partial class SetView {

        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public DateTimeOffset creationDate { get; set; }

        [DataMember]
        public long barrelCount { get; set; }

        [DataMember]
        public long bulletCount { get; set; }
        
        [DataMember]
        public DateTimeOffset? lastScanDate { get; set; }
    }
}