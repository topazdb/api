
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models {
    abstract public class Model {
        
        [NotMapped]
        public List<PropertyInfo> editableProperties {
            get {
                return (from property in GetType().GetProperties() 
                    where property.IsDefined(typeof(api.Attributes.UserEditable), false) == true 
                    select property).ToList();
            }
        }
    }
}