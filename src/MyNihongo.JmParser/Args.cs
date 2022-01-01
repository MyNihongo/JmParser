using CommandLine;

namespace MyNihongo.JmParser;

public sealed class Args
{
	[Option('s', "src")]
	public string SourceFile { get; init; } = string.Empty;

	[Option('d', "dest")]
	public string Destination { get; init; } = string.Empty;
}