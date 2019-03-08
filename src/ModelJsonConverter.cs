using System;
using System.Reflection;
using Newtonsoft.Json;
using api.Models;

namespace api {
    public class ModelJsonConverter : JsonConverter {

        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object existingValue, JsonSerializer serializer) {
            throw new Exception();
        }
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.Null) return null;
            System.Console.WriteLine("test");
            object value = typeof(Model<>).MakeGenericType(objectType).GetMethod("create").Invoke(null, null);
            serializer.Populate(reader, value);
            return value;
        }

        public override bool CanConvert(Type objectType) {
            try {
                return typeof(Model<>).MakeGenericType(objectType).IsAssignableFrom(objectType);
            } catch {
                return false;
            }
        }
    }
}