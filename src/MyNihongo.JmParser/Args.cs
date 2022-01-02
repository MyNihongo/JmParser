using CommandLine;
using MyNihongo.JmParser.Enums;

namespace MyNihongo.JmParser;

public sealed class Args
{
	[Option('s', "src")]
	public string SourceFile { get; init; } = string.Empty;

	[Option('t', "type")]
	public ParseType ParseType { get; init; }

	[Option('d', "dest")]
	public string Destination { get; init; } = string.Empty;
}