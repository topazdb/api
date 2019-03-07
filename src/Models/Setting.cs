using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {

    [Table("settings")]
    [DataContract]
    public partial class Setting {
        [Key]
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string value { get; set; }
    }
}
