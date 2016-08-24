using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace GrafGenerator.ResxLocalize
{
    internal partial class ResxLocalizer
    {
        public static Tuple<string, XDocument>[] Localize(string inputResxPath, JArray stringData, string sourceKey,
            string[] targetKeys)
        {
            var sourceResx = XDocument.Parse(File.ReadAllText(inputResxPath));
            var stringStore = new StringStore(stringData, sourceKey);

            return targetKeys
                .Select(k =>
                    new Tuple<string, XDocument>(k, new ResxLocalizer(sourceResx, stringStore, k).ProduceOutputXml()))
                .ToArray();
        }
    }
}