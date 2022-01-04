namespace MyNihongo.JmParser.Jmdic.Models;

public sealed record JmdicModel
{
	public string Id { get; internal set; } = string.Empty;

	public string Word { get; internal set; } = string.Empty;

	public string Reading { get; internal set; } = string.Empty;

	public IReadOnlyList<string> AlternativeWords { get; internal set; } = Array.Empty<string>();

	public IReadOnlyList<string> AlternativeReadings { get; internal set; } = Array.Empty<string>();

	public IReadOnlyList<Sense> EnglishSenses { get; internal set; } = Array.Empty<Sense>();

	public IReadOnlyList<Sense> DutchSenses { get; internal set; } = Array.Empty<Sense>();
	
	public IReadOnlyList<Sense> FrenchSenses { get; internal set; } = Array.Empty<Sense>();
	
	public IReadOnlyList<Sense> GermanSenses { get; internal set; } = Array.Empty<Sense>();
	
	public IReadOnlyList<Sense> HungarianSenses { get; internal set; } = Array.Empty<Sense>();
	
	public IReadOnlyList<Sense> RussianSenses { get; internal set; } = Array.Empty<Sense>();
	
	public IReadOnlyList<Sense> SlovenianSenses { get; internal set; } = Array.Empty<Sense>();
	
	public IReadOnlyList<Sense> SpanishSenses { get; internal set; } = Array.Empty<Sense>();
	
	public IReadOnlyList<Sense> SwedishSenses { get; internal set; } = Array.Empty<Sense>();

	public sealed record Sense
	{
		public IReadOnlyList<string> StructureTypes { get; internal set; } = Array.Empty<string>();

		public IReadOnlyList<string> ExpressionTypes { get; internal set; } = Array.Empty<string>();

		public string Info { get; internal set; } = string.Empty;
	}
}