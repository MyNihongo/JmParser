using MyNihongo.JmParser.Kanjidic.Models;
using MyNihongo.JmParser.Kanjidic.Services;
using MyNihongo.JmParser.Utils;

namespace MyNihongo.JmParser.Services;

internal sealed class ParsingService
{
	public async Task ParseAsync(Args args)
	{
		if (!File.Exists(args.SourceFile))
			throw new Exception($"File not exists at `{args.SourceFile}`");

		var kanjidic = await ReadKanjidicAsync(args)
			.ConfigureAwait(false);
	}

	private static async Task<KanjidicModel[]> ReadKanjidicAsync(Args args)
	{
		await using var stream = FileUtils.AsyncStream(args.SourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

		return new KanjidicParsingService()
			.Parse(stream);
	}
}