using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    [Table("scans")]
    [DataContract]
    public partial class Scan {
        public Scan() {
            lands = new HashSet<Land>();
        }

        [DataMember]
        public long id { get; set; }

        public long authorId { get; set; }

        public long setId { get; set; }

        public long instrumentId { get; set; }

        [DataMember]
        public long? barrelNo { get; set; }

        [DataMember]
        public long bulletNo { get; set; }

        [DataMember]
        public DateTime creationDate { get; set; }

        [DataMember]
        public int? magnification { get; set; }

        [DataMember]
        public int? threshold { get; set; }

        [DataMember]
        public int? resolution { get; set; }

        [DataMember]
        [ForeignKey("authorId")]
        public Author author { get; set; }
        
        [DataMember]
        [ForeignKey("instrumentId")]
        public Instrument instrument { get; set; }

        [DataMember]
        [ForeignKey("setId")]
        public Set set { get; set; }

        public ICollection<Land> lands { get; set; }
        
        [NotMapped]
        [DataMember]
        public ICollection<long> landIds {
            get {
                return (from land in lands select land.id).ToList();
            }
        }
    }
}
