using System.Text.Json;
using MyNihongo.JmParser.Models;

namespace MyNihongo.JmParser.Utils.Extensions;

internal static class JsonContextEx
{
	public static async ValueTask<string> SerializeAsync<T>(this JsonContext<T> @this, Args args)
	{
		if (string.IsNullOrEmpty(args.Destination))
			return JsonSerializer.Serialize(@this.Data, @this.JsonTypeInfo);

		await using var fileStream = FileUtils.AsyncStream(args.Destination, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

		await JsonSerializer.SerializeAsync(fileStream, @this.Data, @this.JsonTypeInfo)
			.ConfigureAwait(false);

		return string.Empty;
	}
}