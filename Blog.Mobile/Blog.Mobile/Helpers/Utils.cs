using System;

namespace Blog.Mobile.Helpers
{
	public class Utils
	{
		public static string GetBindingNameString(Type type, string property)
		{
			return string.Format ("{0}.{1}", type.Name, property);
		}
	}
}

