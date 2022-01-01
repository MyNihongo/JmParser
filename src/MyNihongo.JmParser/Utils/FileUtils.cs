namespace MyNihongo.JmParser.Utils;

internal static class FileUtils
{
	public static FileStream AsyncStream(string path, FileMode mode, FileAccess access, FileShare share) =>
		new(path, mode, access, share, 4086, true);
}