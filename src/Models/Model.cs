
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.DynamicProxy;

namespace api.Models {
    abstract public class Model<T> where T : Model<T>, new() {

        protected static ProxyGenerator generator = new ProxyGenerator();

        [NotMapped]
        public virtual HashSet<PropertyInfo> dirty { get; set; } = new HashSet<PropertyInfo>();

        public class Interceptor : IInterceptor {
            public void Intercept(IInvocation invocation) {
                
                string[] parts = invocation.Method.Name.Split("_");
                
                if(parts.Length == 2 && parts[0] == "set") {
                    System.Console.WriteLine(parts[1]);
                    var target = invocation.InvocationTarget;
                    var targetType = invocation.TargetType;
                    var prop = targetType.GetProperty(parts[1]);

                    if(prop.IsDefined(typeof(api.Attributes.UserEditable), true)) {
                        ((T) target).dirty.Add(prop);
                    }
                } 

                invocation.Proceed();
            }
        }
        

        public static T create() {
            return (T) generator.CreateClassProxyWithTarget(typeof(T), new T(), new Interceptor());
        }

        public virtual Model<T> merge(T source) {      
            foreach(PropertyInfo prop in source.dirty) {
                var newValue = prop.GetValue(source);
                System.Console.WriteLine(newValue);
                prop.SetValue(this, Convert.ChangeType(newValue, prop.PropertyType), null);
            }            
            
            return this;
        }
    }
}