using System.Xml.Serialization;
using MyNihongo.JmParser.Kanjidic.Models;

namespace MyNihongo.JmParser.Kanjidic.Services;

public sealed class KanjidicParsingService : IKanjidicParsingService
{
	public KanjidicModel[] Parse(Stream stream)
	{
		using var reader = new StreamReader(stream);

		var serializer = new XmlSerializer(typeof(KanjidicXmlModel));
		var q = serializer.Deserialize(reader);
		var qq = "a";

		return Array.Empty<KanjidicModel>();
	}
}