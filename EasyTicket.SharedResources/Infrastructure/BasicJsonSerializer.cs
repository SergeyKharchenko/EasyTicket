using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EasyTicket.SharedResources.Infrastructure {
    public static class BasicJsonSerializer {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        public static string Serialize<T>(T obj) where T: class {
            try {
                return JsonConvert.SerializeObject(obj, Formatting.Indented, SerializerSettings);
            } catch {
                return null;
            }
        }
        public static T Deserialize<T>(string json) where T: class {
            try {
                return JsonConvert.DeserializeObject<T>(json, SerializerSettings);
            } catch {
                return null;
            }
        }
    }
}