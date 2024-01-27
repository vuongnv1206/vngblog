using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using VngBlog.Contract.Common;

namespace VngBlog.Infrastructure.Common
{
	public class SerializeService : ISerializeService
	{

		public string Serialize<T>(T entity)
		{
			return JsonConvert.SerializeObject(entity, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(), //viết hoa từ chữ thư 2
				NullValueHandling = NullValueHandling.Ignore,  //value null thì bỏ qua
				Converters = new List<JsonConverter>
				{
					new StringEnumConverter    //Enum thì trả theo định dang Camelcase
					{
						NamingStrategy = new CamelCaseNamingStrategy()
					}
				}
			});
		}

		public string Serialize<T>(T entity, Type type)
		{
			return JsonConvert.SerializeObject(entity, type, new JsonSerializerSettings());
		}

		public T Deserialize<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
