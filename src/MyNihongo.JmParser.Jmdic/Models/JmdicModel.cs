namespace MyNihongo.JmParser.Jmdic.Models;

public sealed record JmdicModel
{
	public string Id { get; internal set; } = string.Empty;

	public string Word { get; internal set; } = string.Empty;

	public string Reading { get; internal set; } = string.Empty;

	public IReadOnlyList<string> AlternativeWords { get; internal set; } = Array.Empty<string>();

	public IReadOnlyList<string> AlternativeReadings { get; internal set; } = Array.Empty<string>();
}