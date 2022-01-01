using MyNihongo.JmParser.Kanjidic.Models;

namespace MyNihongo.JmParser.Kanjidic.Services;

public interface IKanjidicParsingService
{
	KanjidicModel[] Parse(Stream stream);
}