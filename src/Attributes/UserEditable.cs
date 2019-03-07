
namespace api.Attributes {
    [System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
    public class UserEditable : System.Attribute {
        public UserEditable() {}
    }
}