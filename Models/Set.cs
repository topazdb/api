using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {

    [Table("sets")]
    [DataContract]
    public partial class Set {
        public Set() {
            scans = new HashSet<Scan>();
        }

        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }
        
        [DataMember]
        public DateTimeOffset creationDate { get; set; }

        public ICollection<Scan> scans { get; set; }
    }
}
