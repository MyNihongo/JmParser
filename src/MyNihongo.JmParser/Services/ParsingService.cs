using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml.Linq;
using MyNihongo.JmParser.Enums;
using MyNihongo.JmParser.Jmdic.Models;
using MyNihongo.JmParser.Jmdic.Services;
using MyNihongo.JmParser.Kanjidic.Models;
using MyNihongo.JmParser.Kanjidic.Services;
using MyNihongo.JmParser.Models;
using MyNihongo.JmParser.Utils;
using MyNihongo.JmParser.Utils.Extensions;

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

		var task = args.ParseType switch
		{
			ParseType.Kanjidic => ParseKanjidic(xml).SerializeAsync(args),
			ParseType.Jmdic => ParseJmdic(xml).SerializeAsync(args),
			_ => throw new InvalidOperationException($"Unknown {nameof(ParseType)}: {args.ParseType}")
		};

		var stringData = await task.ConfigureAwait(false);
	}

	private static async Task<XDocument> ParseXml(Args args, CancellationToken ct = default)
	{
		await using var stream = FileUtils.AsyncStream(args.SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

		return await XDocument.LoadAsync(stream, LoadOptions.None, ct)
			.ConfigureAwait(false);
	}

	private JsonContext<KanjidicModel> ParseKanjidic(XDocument xml)
	{
		var data = new KanjidicParsingService()
			.Parse(xml);

		var jsonTypeInfo = new KanjidicModelContext(_jsonOptions)
			.IEnumerableKanjidicModel;

		return new JsonContext<KanjidicModel>(data, jsonTypeInfo);
	}

	private JsonContext<JmdicModel> ParseJmdic(XDocument xml)
	{
		var data = new JmdicParsingService()
			.Parse(xml);

		var jsonTypeInfo = new JmdicModelContext(_jsonOptions)
			.IEnumerableJmdicModel;

		return new JsonContext<JmdicModel>(data, jsonTypeInfo);
	}
}