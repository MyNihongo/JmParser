using System.Xml.Linq;
using MyNihongo.JmParser.Kanjidic.Enums;
using MyNihongo.JmParser.Kanjidic.Models;
using MyNihongo.JmParser.Kanjidic.Resources;

namespace MyNihongo.JmParser.Kanjidic.Services;

public sealed class KanjidicParsingService : IKanjidicParsingService
{
	public IEnumerable<KanjidicModel> Parse(XDocument xDocument)
	{
		var list = new List<KanjidicModel>(13_108);
		KanjidicModel? current = null;
		List<string> kunYomi = new(), onYomi = new();

		foreach (var xElement in xDocument.DescendantNodes().OfType<XElement>())
		{
			switch (xElement.Name.LocalName)
			{
				case "character":
					{
						if (current != null)
						{
							current.KunYomi = kunYomi.ToArray();
							current.OnYomi = onYomi.ToArray();
							list.Add(current);
						}

						current = new KanjidicModel();
						kunYomi.Clear();
						onYomi.Clear();
						break;
					}
				case "literal":
					{
						if (xElement.Value.Length != 1)
						{
							current = null;
							Console.WriteLine("Invalid length");

							goto case "character";
						}

						current!.Character = xElement.Value[0];
						break;
					}
				case "jlpt":
					{
						current!.JlptLevel = GetJlptLevel(xElement.Value);
						break;
					}
				case "reading":
					{
						switch (TryGetReading(xElement, out var reading))
						{
							case ReadingType.KunYomi:
								kunYomi.Add(reading);
								break;
							case ReadingType.OnYomi:
								onYomi.Add(reading);
								break;
						}

						break;
					}
				case "meaning":
					break;
			}
		}

		return list;
	}

	public static byte GetJlptLevel(string jlptStriing)
	{
		var jlpt = byte.Parse(jlptStriing);
		
		// old level 2 is divided between N2 and N3
		if (jlpt >= 3)
			jlpt++;

		return jlpt;
	}

	private static ReadingType? TryGetReading(XElement xElement, out string reading)
	{
		reading = xElement.Value.Replace('.', '|');

		if (string.IsNullOrEmpty(xElement.Value))
			return null;

		return xElement.Attribute(XNames.ReadingType)?.Value switch
		{
			"ja_on" => ReadingType.OnYomi,
			"ja_kun" => ReadingType.KunYomi,
			_ => null
		};
	}
}