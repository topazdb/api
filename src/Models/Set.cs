using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {

    [Table("sets")]
    [DataContract]
    public partial class Set : Model<Set> {
        public Set() {
            scans = new HashSet<Scan>();
            subsets = new HashSet<Set>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long id { get; set; }

        [DataMember]
        [UserEditable]
        [ForeignKey("id")]
        public virtual long? parentId { get; set; }

        [DataMember]
        [UserEditable]
        public virtual string name { get; set; }
        
        [DataMember]
        [UserEditable]
        public virtual string childPrefix { get; set; }
        
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual DateTimeOffset creationDate { get; set; }

        [DataMember]
        [ForeignKey("parentId")]
        public virtual ICollection<Set> subsets { get; set; }

        [DataMember]
        public virtual ICollection<Scan> scans { get; set; }

        [DataMember]
        [NotMapped]
        public virtual Set parent { get; set; }
    }
}
