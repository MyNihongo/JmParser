using System.Text.Json.Serialization;

namespace MyNihongo.JmParser.Kanjidic.Models;

[JsonSerializable(typeof(IEnumerable<KanjidicModel>))]
public partial class KanjidicModelContext : JsonSerializerContext
{
}

public sealed record KanjidicModel
{
	public int Id { get; internal set; }

	[JsonPropertyName("c")]
	public char Character { get; internal set; }

	[JsonPropertyName("jlpt")]
	public byte JlptLevel { get; internal set; }

	public IReadOnlyList<string> KunYomi { get; internal set; } = Array.Empty<string>();
	
	public IReadOnlyList<string> OnYomi { get; internal set; } = Array.Empty<string>();

	[JsonPropertyName("en")]
	public IReadOnlyList<string> English { get; internal set; } = Array.Empty<string>();
	
	[JsonPropertyName("fr")]
	public IReadOnlyList<string> French { get; internal set; } = Array.Empty<string>();
	
	[JsonPropertyName("es")]
	public IReadOnlyList<string> Spanish { get; internal set; } = Array.Empty<string>();
	
	[JsonPropertyName("pt")]
	public IReadOnlyList<string> Portuguese { get; internal set; } = Array.Empty<string>();
}