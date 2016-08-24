using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace GrafGenerator.ResxLocalize.ResxLocalizeCommand
{
    public partial class ResxLocalizer
    {
        public static Tuple<string, XDocument>[] Localize(XDocument inputResx, JArray stringData, string sourceKey,
            string[] targetKeys)
        {
            var stringStore = new StringStore(stringData, sourceKey);

            return targetKeys
                .Select(k =>
                    new Tuple<string, XDocument>(k, new ResxLocalizer(inputResx, stringStore, k).ProduceOutputXml()))
                .ToArray();
        }

        public static Tuple<string, XDocument>[] Localize(string inputResxPath, JArray stringData, string sourceKey,
            string[] targetKeys)
        {
            var inputResx = XDocument.Parse(File.ReadAllText(inputResxPath));

            return Localize(inputResx, stringData, sourceKey, targetKeys);
        }
    }
}