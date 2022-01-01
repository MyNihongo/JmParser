using System.Xml.Linq;
using MyNihongo.JmParser.Kanjidic.Services;
using MyNihongo.JmParser.Utils;

namespace MyNihongo.JmParser.Services;

internal sealed class ParsingService
{
	public async Task ParseAsync(Args args)
	{
		if (!File.Exists(args.SourceFile))
			throw new Exception($"File not exists at `{args.SourceFile}`");

		var xml = await ParseXml(args)
			.ConfigureAwait(false);

		var items = new KanjidicParsingService()
			.Parse(xml);
	}

	private static async Task<XDocument> ParseXml(Args args, CancellationToken ct = default)
	{
		await using var stream = FileUtils.AsyncStream(args.SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

		return await XDocument.LoadAsync(stream, LoadOptions.None, ct)
			.ConfigureAwait(false);
	}
}