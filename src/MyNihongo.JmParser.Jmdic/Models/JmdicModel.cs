using System.Text.Json.Serialization;

namespace MyNihongo.JmParser.Jmdic.Models;

[JsonSerializable(typeof(IEnumerable<JmdicModel>))]
public partial class JmdicModelContext : JsonSerializerContext
{
}

public sealed record JmdicModel
{
	public string Id { get; internal set; } = string.Empty;

	[JsonPropertyName("w")]
	public string Word { get; internal set; } = string.Empty;

	[JsonPropertyName("r")]
	public string Reading { get; internal set; } = string.Empty;

	[JsonPropertyName("aw")]
	public IReadOnlyList<string> AlternativeWords { get; internal set; } = Array.Empty<string>();

	[JsonPropertyName("ar")]
	public IReadOnlyList<string> AlternativeReadings { get; internal set; } = Array.Empty<string>();

	[JsonPropertyName("en")]
	public IReadOnlyList<Sense> EnglishSenses { get; internal set; } = Array.Empty<Sense>();

	[JsonPropertyName("nl")]
	public IReadOnlyList<Sense> DutchSenses { get; internal set; } = Array.Empty<Sense>();
	
	[JsonPropertyName("fr")]
	public IReadOnlyList<Sense> FrenchSenses { get; internal set; } = Array.Empty<Sense>();
	
	[JsonPropertyName("de")]
	public IReadOnlyList<Sense> GermanSenses { get; internal set; } = Array.Empty<Sense>();
	
	[JsonPropertyName("hu")]
	public IReadOnlyList<Sense> HungarianSenses { get; internal set; } = Array.Empty<Sense>();
	
	[JsonPropertyName("ru")]
	public IReadOnlyList<Sense> RussianSenses { get; internal set; } = Array.Empty<Sense>();
	
	[JsonPropertyName("sl")]
	public IReadOnlyList<Sense> SlovenianSenses { get; internal set; } = Array.Empty<Sense>();
	
	[JsonPropertyName("es")]
	public IReadOnlyList<Sense> SpanishSenses { get; internal set; } = Array.Empty<Sense>();
	
	[JsonPropertyName("se")]
	public IReadOnlyList<Sense> SwedishSenses { get; internal set; } = Array.Empty<Sense>();

	public sealed record Sense
	{
		[JsonPropertyName("st")]
		public IReadOnlyList<string> StructureTypes { get; internal set; } = Array.Empty<string>();

		[JsonPropertyName("et")]
		public IReadOnlyList<string> ExpressionTypes { get; internal set; } = Array.Empty<string>();

		[JsonPropertyName("i")]
		public string Info { get; internal set; } = string.Empty;

		[JsonPropertyName("gl")]
		public IReadOnlyList<string> Glossaries { get; internal set; } = Array.Empty<string>();
	}
}