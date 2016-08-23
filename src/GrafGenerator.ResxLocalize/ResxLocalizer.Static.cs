using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace GrafGenerator.ResxLocalize
{
    internal partial class ResxLocalizer
    {
        public static XDocument[] Localize(string inputResxPath, JArray stringData, string sourceKey,
            string[] targetKeys)
        {
            var sourceResx = XDocument.Parse(File.ReadAllText(inputResxPath));
            var stringStore = new StringStore(stringData, sourceKey);

            return targetKeys
                .Select(k => new ResxLocalizer(sourceResx, stringStore, k).ProduceOutputXml())
                .ToArray();
        }
    }
}