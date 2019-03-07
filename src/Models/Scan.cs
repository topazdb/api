using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {
    [Table("scans")]
    [DataContract]
    public partial class Scan : Model {
        public Scan() {
            lands = new HashSet<Land>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [UserEditable]
        public long authorId { get; set; }

        [UserEditable]
        public long setId { get; set; }

        [UserEditable]
        public long instrumentId { get; set; }

        [DataMember]
        [UserEditable]
        public long? barrelNo { get; set; }

        [DataMember]
        [UserEditable]
        public long bulletNo { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime creationDate { get; set; }

        [DataMember]
        [UserEditable]
        public int? magnification { get; set; }

        [DataMember]
        [UserEditable]
        public int? threshold { get; set; }

        [DataMember]
        [UserEditable]
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
