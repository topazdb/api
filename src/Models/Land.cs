using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {
    [Table("lands")]
    [DataContract]
    public partial class Land : Model {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [DataMember]
        [UserEditable]
        public long scanId { get; set; }

        [UserEditable]
        public string path { get; set; }

        public Scan scan { get; set; }
    }
}
