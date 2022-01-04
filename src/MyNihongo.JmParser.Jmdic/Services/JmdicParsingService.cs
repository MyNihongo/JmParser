using System.Xml.Linq;
using MyNihongo.JmParser.Jmdic.Enums;
using MyNihongo.JmParser.Jmdic.Models;

namespace MyNihongo.JmParser.Jmdic.Services;

public sealed class JmdicParsingService : IJmdicParsingService
{
	public IEnumerable<JmdicModel> Parse(XDocument xDocument)
	{
		var entities = new Dictionary<string, string>(239);

		JmdicModel? current = null;
		List<string> alternativeWords = new(), alternativeReadings = new();
		List<JmdicModel.Sense> english = new(), dutch = new(), french = new(), german = new(), hungarian = new(), russian = new(), slovenian = new(), spanish = new(), swedish = new();

		JmdicModel.Sense? currentSense = null;
		Language? currentLanguage = null;
		List<string> structures = new(), expressions = new(), glossaries = new();

		foreach (var xNode in xDocument.DescendantNodes())
		{
			switch (xNode)
			{
				case XDocumentType xDocumentType when entities.Count == 0:
					{
						entities = LoadEntities(entities, xDocumentType);
						break;

					}
				case XElement xElement:
					{
						switch (xElement.Name.LocalName)
						{
							case "entry":
								{
									if (!string.IsNullOrEmpty(current?.Id))
									{
										AppendSense(currentSense);
										currentSense = null;

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
									AppendSense(currentSense);
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
									var value = UnwrapHtmlSymbol(entities, xElement.Value);
									structures.Add(value);
									break;
								}
							case "misc":
								{
									var value = UnwrapHtmlSymbol(entities, xElement.Value);
									expressions.Add(value);
									break;
								}
							case "s_inf":
								{
									currentSense!.Info = xElement.Value;
									break;
								}
						}
						break;
					}
			}
		}

		void AppendSense(JmdicModel.Sense? sense)
		{
			// there might be empty <sense> tags
			if (sense != null && currentLanguage != null)
			{
				sense.StructureTypes = structures.ToArray();
				sense.ExpressionTypes = expressions.ToArray();
				sense.Glossaries = glossaries.ToArray();

				switch (currentLanguage)
				{
					case Language.English:
						english.Add(sense);
						break;
					case Language.Dutch:
						dutch.Add(sense);
						break;
					case Language.French:
						french.Add(sense);
						break;
					case Language.German:
						german.Add(sense);
						break;
					case Language.Hungarian:
						hungarian.Add(sense);
						break;
					case Language.Russian:
						russian.Add(sense);
						break;
					case Language.Slovenian:
						slovenian.Add(sense);
						break;
					case Language.Spanish:
						spanish.Add(sense);
						break;
					case Language.Swedish:
						swedish.Add(sense);
						break;
					default:
						throw new InvalidOperationException("Language is not defined");
				}

				currentLanguage = null;
			}

			structures.Clear();
			expressions.Clear();
			glossaries.Clear();
		}
	}

	private static Dictionary<string, string> LoadEntities(Dictionary<string, string> dictionary, XDocumentType xDocumentType)
	{
		const string entryStart = "<!ENTITY";

		if (!string.IsNullOrEmpty(xDocumentType.InternalSubset))
		{
			var lines = xDocumentType.InternalSubset
				.Split('\n', StringSplitOptions.TrimEntries)
				.Where(static x => x.StartsWith(entryStart));

			foreach (var line in lines)
			{
				int splitIndex = line.IndexOf('"', entryStart.Length),
					endIndex = line.LastIndexOf('"');

				var key = line[(splitIndex + 1)..endIndex].Trim();
				var value = line[entryStart.Length..splitIndex].Trim();

				dictionary[key] = value;
			}
		}

		return dictionary;
	}

	private static string UnwrapHtmlSymbol(IReadOnlyDictionary<string, string> entities, in string entityValue)
	{
		if (entities.TryGetValue(entityValue, out var value))
			return value;

		throw new InvalidOperationException("Not a valid HTML symbol");
	}

	private static Language GetGlossary(in XElement xElement, out string value)
	{
		value = xElement.Value;

		foreach (var xAttribute in xElement.Attributes())
		{
			if (xAttribute.Name.LocalName != "lang")
				continue;

			return xAttribute.Value switch
			{
				"dut" => Language.Dutch,
				"fre" => Language.French,
				"ger" => Language.German,
				"hun" => Language.Hungarian,
				"rus" => Language.Russian,
				"slv" => Language.Slovenian,
				"spa" => Language.Spanish,
				"swe" => Language.Swedish,
				"eng" => Language.English,
				_ => throw new InvalidOperationException($"Unknown {nameof(Language)}")
			};
		}

		return Language.English;
	}
}