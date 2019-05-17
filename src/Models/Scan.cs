using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {
    [Table("scans")]
    [DataContract]
    public partial class Scan : Model<Scan> {
        public Scan() {
            lands = new HashSet<Land>();
        }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long id { get; set; }

        [UserEditable]
        [DataMember]
        public virtual long authorId { get; set; }

        [UserEditable]
        [DataMember]
        public virtual long setId { get; set; }

        [UserEditable]
        [DataMember]
        public virtual long instrumentId { get; set; }

        [UserEditable]
        [DataMember]
        public virtual long scanNo { get; set; }

        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual DateTime creationDate { get; set; }

        [DataMember]
        [UserEditable]
        public virtual int? magnification { get; set; }

        [DataMember]
        [UserEditable]
        public virtual int? threshold { get; set; }

        [DataMember]
        [UserEditable]
        public virtual int? resolution { get; set; }

        [DataMember]
        [ForeignKey("authorId")]
        public virtual Author author { get; set; }
        
        [DataMember]
        [ForeignKey("instrumentId")]
        public virtual Instrument instrument { get; set; }

        [DataMember]
        [ForeignKey("setId")]
        public virtual Set set { get; set; }

        public virtual ICollection<Land> lands { get; set; }
        
        [NotMapped]
        [DataMember]
        public virtual ICollection<long> landIds {
            get {
                return (from land in lands select land.id).ToList();
            }
        }
    }
}
