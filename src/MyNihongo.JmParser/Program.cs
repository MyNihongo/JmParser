using CommandLine;
using MyNihongo.JmParser;
using MyNihongo.JmParser.Services;

await Parser.Default
	.ParseArguments<Args>(args)
	.WithParsedAsync(new ParsingService().ParseAsync);
