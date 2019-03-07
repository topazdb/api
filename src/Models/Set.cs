using System;
using System.Linq;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [DataMember]
        public string name { get; set; }
        
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset creationDate { get; set; }

        public ICollection<Scan> scans { get; set; }

        [NotMapped]
        [DataMember]
        public int barrelCount {
            get {
                return (from scan in scans select scan.barrelNo).Distinct().Count();
            }
        }

        [NotMapped]
        [DataMember]
        public int bulletCount {
            get {
                return (from scan in scans select (scan.barrelNo, scan.bulletNo)).Distinct().Count();
            }
        }
    }
}
