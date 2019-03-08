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
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long id { get; set; }

        [DataMember]
        [UserEditable]
        public virtual string name { get; set; }
        
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual DateTimeOffset creationDate { get; set; }

        public virtual ICollection<Scan> scans { get; set; }

        [NotMapped]
        [DataMember]
        public virtual int barrelCount {
            get {
                return (from scan in scans select scan.barrelNo).Distinct().Count();
            }
        }

        [NotMapped]
        [DataMember]
        public virtual int bulletCount {
            get {
                return (from scan in scans select (scan.barrelNo, scan.bulletNo)).Distinct().Count();
            }
        }
    }
}
