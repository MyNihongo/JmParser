namespace MyNihongo.JmParser.Kanjidic.Models;

public sealed record KanjidicModel
{
	public char Character { get; internal set; }

	public byte JlptLevel { get; internal set; }
}