using CommandLine;
using MyNihongo.JmParser;
using MyNihongo.JmParser.Services;

var parser = new Parser(static x =>
{
	x.CaseInsensitiveEnumValues = true;
	x.HelpWriter = Console.Error;
});

await parser.ParseArguments<Args>(args)
	.WithParsedAsync(new ParsingService().ParseAsync);
