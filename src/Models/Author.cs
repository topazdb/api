using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {
    [Table("authors")]
    [DataContract]
    public partial class Author : Model<Author> {
        public Author() {
            scans = new HashSet<Scan>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long id { get; set; }

        [DataMember]
        [UserEditable]
        public virtual string name { get; set; }

        [DataMember]
        [UserEditable]
        public virtual string contact { get; set; }

        public virtual ICollection<Scan> scans { get; set; }
    }
}
