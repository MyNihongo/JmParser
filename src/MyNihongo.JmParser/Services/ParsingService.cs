using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml.Linq;
using MyNihongo.JmParser.Enums;
using MyNihongo.JmParser.Jmdic.Models;
using MyNihongo.JmParser.Jmdic.Services;
using MyNihongo.JmParser.Kanjidic.Models;
using MyNihongo.JmParser.Kanjidic.Services;
using MyNihongo.JmParser.Utils;

namespace MyNihongo.JmParser.Services;

internal sealed class ParsingService
{
	private readonly JsonSerializerOptions _jsonOptions = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
		WriteIndented = true
	};

	public async Task ParseAsync(Args args)
	{
		if (!File.Exists(args.SourceFile))
			throw new Exception($"File not exists at `{args.SourceFile}`");

		var xml = await ParseXml(args)
			.ConfigureAwait(false);

		object data = args.ParseType switch
		{
			ParseType.Kanjidic => ParseKanjidic(xml),
			ParseType.Jmdic => ParseJmdic(xml),
			_ => throw new InvalidOperationException($"Unknown {nameof(ParseType)}: {args.ParseType}")
		};

		var stringValue = JsonSerializer.Serialize(data, _jsonOptions);
		var a = "";
	}

	private static async Task<XDocument> ParseXml(Args args, CancellationToken ct = default)
	{
		await using var stream = FileUtils.AsyncStream(args.SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

		return await XDocument.LoadAsync(stream, LoadOptions.None, ct)
			.ConfigureAwait(false);
	}

	private static IEnumerable<KanjidicModel> ParseKanjidic(XDocument xml) =>
		new KanjidicParsingService()
			.Parse(xml);

	private static IEnumerable<JmdicModel> ParseJmdic(XDocument xml) =>
		new JmdicParsingService()
			.Parse(xml);
}