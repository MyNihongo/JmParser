namespace MyNihongo.JmParser.Kanjidic.Models;

public sealed record KanjidicModel
{
	public char Character { get; internal set; }

	public byte JlptLevel { get; internal set; }

	public IReadOnlyList<string> KunYomi { get; internal set; } = Array.Empty<string>();
	
	public IReadOnlyList<string> OnYomi { get; internal set; } = Array.Empty<string>();

	public IReadOnlyList<string> English { get; internal set; } = Array.Empty<string>();
	
	public IReadOnlyList<string> French { get; internal set; } = Array.Empty<string>();
	
	public IReadOnlyList<string> Spanish { get; internal set; } = Array.Empty<string>();
	
	public IReadOnlyList<string> Portuguese { get; internal set; } = Array.Empty<string>();
}