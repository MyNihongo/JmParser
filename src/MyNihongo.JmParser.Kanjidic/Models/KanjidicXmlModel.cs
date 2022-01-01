using System.Xml.Serialization;

namespace MyNihongo.JmParser.Kanjidic.Models;

[XmlRoot("kanjidic2")]
internal sealed record KanjidicXmlModel
{
	[XmlArrayItem("character")]
	public KanjidicModel[] Kanji { get; init; } = Array.Empty<KanjidicModel>();
}