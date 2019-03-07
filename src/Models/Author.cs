using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {
    [Table("authors")]
    [DataContract]
    public partial class Author : Model {
        public Author() {
            scans = new HashSet<Scan>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [DataMember]
        [UserEditable]
        public string name { get; set; }

        [DataMember]
        [UserEditable]
        public string contact { get; set; }

        public ICollection<Scan> scans { get; set; }
    }
}
