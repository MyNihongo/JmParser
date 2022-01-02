using System.Xml.Linq;
using MyNihongo.JmParser.Jmdic.Models;

namespace MyNihongo.JmParser.Jmdic.Services;

public interface IJmdicParsingService
{
	IEnumerable<JmdicModel> Parse(XDocument xDocument);
}