using System.Xml.Linq;
using MyNihongo.JmParser.Jmdic.Enums;
using MyNihongo.JmParser.Jmdic.Models;
using MyNihongo.JmParser.Jmdic.Resources;

namespace MyNihongo.JmParser.Jmdic.Services;

public sealed class JmdicParsingService : IJmdicParsingService
{
	public IEnumerable<JmdicModel> Parse(XDocument xDocument)
	{
		JmdicModel? current = null;
		List<string> alternativeWords = new(), alternativeReadings = new();
		List<JmdicModel.Sense> english = new(), dutch = new(), french = new(), german = new(), hungarian = new(), russian = new(), slovenian = new(), spanish = new(), swedish = new();

		JmdicModel.Sense? currentSense = null;
		Language? currentLanguage = null;
		List<string> structures = new(), expressions = new(), glossaries = new();

		foreach (var xElement in xDocument.DescendantNodes().OfType<XElement>())
			switch (xElement.Name.LocalName)
			{
				case "entry":
					{
						if (!string.IsNullOrEmpty(current?.Id))
						{
							currentSense = null;
							currentLanguage = null;

							current.AlternativeWords = alternativeWords.ToArray();
							current.AlternativeReadings = alternativeReadings.ToArray();
							current.EnglishSenses = english.ToArray();
							current.DutchSenses = dutch.ToArray();
							current.FrenchSenses = french.ToArray();
							current.GermanSenses = german.ToArray();
							current.HungarianSenses = hungarian.ToArray();
							current.RussianSenses = russian.ToArray();
							current.SlovenianSenses = slovenian.ToArray();
							current.SpanishSenses = spanish.ToArray();
							current.SwedishSenses = swedish.ToArray();
							yield return current;
						}

						alternativeWords.Clear();
						alternativeReadings.Clear();
						english.Clear();
						dutch.Clear();
						french.Clear();
						german.Clear();
						hungarian.Clear();
						russian.Clear();
						slovenian.Clear();
						spanish.Clear();
						swedish.Clear();
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
				case "sense":
					{
						if (currentSense != null)
						{
							currentSense.StructureTypes = structures.ToArray();
							currentSense.ExpressionTypes = expressions.ToArray();
							currentSense.Glossaries = glossaries.ToArray();

							switch (currentLanguage)
							{
								case Language.English:
									english.Add(currentSense);
									break;
								case Language.Dutch:
									dutch.Add(currentSense);
									break;
								case Language.French:
									french.Add(currentSense);
									break;
								case Language.German:
									german.Add(currentSense);
									break;
								case Language.Hungarian:
									hungarian.Add(currentSense);
									break;
								case Language.Russian:
									russian.Add(currentSense);
									break;
								case Language.Slovenian:
									slovenian.Add(currentSense);
									break;
								case Language.Spanish:
									spanish.Add(currentSense);
									break;
								case Language.Swedish:
									swedish.Add(currentSense);
									break;
								default:
									throw new InvalidOperationException("Language is not defined");
							}

							currentLanguage = null;
						}

						structures.Clear();
						expressions.Clear();
						glossaries.Clear();
						currentSense = new JmdicModel.Sense();
						break;
					}
				case "gloss":
					{
						currentLanguage = GetGlossary(xElement, out var glossary);
						glossaries.Add(glossary);

						break;
					}
				case "pos":
					{
						var value = UnwrapHtmlSymbol(xElement.Value);
						structures.Add(value);
						break;
					}
				case "misc":
					{
						var value = UnwrapHtmlSymbol(xElement.Value);
						expressions.Add(value);
						break;
					}
				case "s_inf":
					{
						currentSense!.Info = xElement.Value;
						break;
					}
			}
	}

	private static string UnwrapHtmlSymbol(in string symbol)
	{
		if (symbol[0] == '&' && symbol[^1] == ';')
			return symbol[1..^1];

		throw new InvalidOperationException("Not a valid HTML symbol");
	}

	private static Language GetGlossary(in XElement xElement, out string value)
	{
		value = xElement.Value;

		return xElement.Attribute(XNames.Language)?.Value switch
		{
			"dut" => Language.Dutch,
			"fre" => Language.French,
			"ger" => Language.German,
			"hun" => Language.Hungarian,
			"rus" => Language.Russian,
			"slv" => Language.Slovenian,
			"spa" => Language.Spanish,
			"swe" => Language.Swedish,
			null => Language.English,
			_ => throw new InvalidOperationException($"Unknown {nameof(Language)}")
		};
	}
}