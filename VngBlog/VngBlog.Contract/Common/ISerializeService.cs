using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Contract.Common
{
	public interface ISerializeService
	{
		string Serialize<T>(T entity);
		string Serialize<T>(T entity, Type type);
		T Deserialize<T>(string json);
	}
}
