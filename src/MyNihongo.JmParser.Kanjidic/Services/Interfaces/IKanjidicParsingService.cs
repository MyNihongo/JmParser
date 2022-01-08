using System.Xml.Linq;
using MyNihongo.JmParser.Kanjidic.Models;

namespace MyNihongo.JmParser.Kanjidic.Services;

public interface IKanjidicParsingService
{
	IEnumerable<KanjidicModel> Parse(XDocument xDocument);
}