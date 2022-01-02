using System.Xml.Linq;
using MyNihongo.JmParser.Jmdic.Models;

namespace MyNihongo.JmParser.Jmdic.Services;

public sealed class JmdicParsingService : IJmdicParsingService
{
	public IEnumerable<JmdicModel> Parse(XDocument xDocument)
	{
		JmdicModel? current = null;
		List<string> alternativeWords = new(), alternativeReadings = new();

		foreach (var xElement in xDocument.DescendantNodes().OfType<XElement>())
			switch (xElement.Name.LocalName)
			{
				case "entry":
					{
						if (!string.IsNullOrEmpty(current?.Id))
						{
							current.AlternativeWords = alternativeWords.ToArray();
							current.AlternativeReadings = alternativeReadings.ToArray();
							yield return current;
						}

						alternativeWords.Clear();
						alternativeReadings.Clear();
						current = new JmdicModel();
						break;
					}
				case "ent_seq":
					{
						current!.Id = xElement.Value;
						break;
					}
				case "keb":
					{
						if (string.IsNullOrEmpty(current!.Word))
							current.Word = xElement.Value;
						else
							alternativeWords.Add(xElement.Value);

						break;
					}
				case "reb":
					{
						if (string.IsNullOrEmpty(current!.Reading))
							current.Reading = xElement.Value;
						else
							alternativeReadings.Add(xElement.Value);

						break;
					}
			}
	}
}