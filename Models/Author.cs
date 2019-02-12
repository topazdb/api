using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    [Table("authors")]
    [DataContract]
    public partial class Author {
        public Author() {
            scans = new HashSet<Scan>();
        }

        [DataMember]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string contact { get; set; }

        public ICollection<Scan> scans { get; set; }
    }
}
