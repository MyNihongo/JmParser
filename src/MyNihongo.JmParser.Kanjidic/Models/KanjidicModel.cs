namespace MyNihongo.JmParser.Kanjidic.Models;

public sealed record KanjidicModel
{
	public char Character { get; internal set; }

	public byte JlptLevel { get; internal set; }

	public IReadOnlyList<string> KunYomi { get; internal set; } = Array.Empty<string>();
	
	public IReadOnlyList<string> OnYomi { get; internal set; } = Array.Empty<string>();
}