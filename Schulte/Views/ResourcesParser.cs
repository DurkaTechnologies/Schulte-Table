using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Schulte.Views
{
	public static class ResourcesParser
	{
		public static ResourceDictionary GetResourceDictionary(string dictionaryUri)
		{
			Uri newUri;
			if (Uri.TryCreate(dictionaryUri, UriKind.Absolute, out newUri))
				return new ResourceDictionary() { Source = newUri };
			else
				return new ResourceDictionary();
		}

		public static IEnumerable<Style> GetStylesFromResourcesDictionary(string dictionaryUri)
		{
			ResourceDictionary tempDictionary = GetResourceDictionary(dictionaryUri);
			if (tempDictionary.Count == 0)
				return null;
			else
				return tempDictionary.Values.OfType<Style>();
		}
	}
}
//