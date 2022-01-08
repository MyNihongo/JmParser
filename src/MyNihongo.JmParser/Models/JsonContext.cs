using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.JmParser.Models;

internal sealed record JsonContext<T>(IEnumerable<T> Data, JsonTypeInfo<IEnumerable<T>> JsonTypeInfo);
