using System.Xml.Linq;
using MyNihongo.JmParser.Kanjidic.Models;

namespace MyNihongo.JmParser.Kanjidic.Services;

public sealed class KanjidicParsingService : IKanjidicParsingService
{
	public IEnumerable<KanjidicModel> Parse(XDocument xDocument)
	{
		var list = new List<KanjidicModel>(13_108);
		KanjidicModel? current = null;

		foreach (var xElement in xDocument.DescendantNodes().OfType<XElement>())
		{
			switch (xElement.Name.LocalName)
			{
				case "character":
					{
						if (current != null)
							list.Add(current);

						current = new KanjidicModel();
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
						current!.JlptLevel = byte.Parse(xElement.Value);
						break;
					}
				case "reading":
					break;
				case "meaning":
					break;
			}
		}

		return list;
	}
}