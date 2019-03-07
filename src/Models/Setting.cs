using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using api.Attributes;

namespace api.Models {

    [Table("settings")]
    [DataContract]
    public partial class Setting : Model {
        [Key]
        [DataMember]
        [UserEditable]
        public string name { get; set; }

        [DataMember]
        [UserEditable]
        public string value { get; set; }
    }
}
