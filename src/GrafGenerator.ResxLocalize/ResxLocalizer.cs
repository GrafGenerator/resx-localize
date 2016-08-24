using System.Xml.Linq;
using System.Xml.XPath;

namespace GrafGenerator.ResxLocalize
{
    public partial class ResxLocalizer
    {
        private ResxLocalizer(XDocument sourceResx, StringStore stringStore, string targetKey)
        {
            Store = stringStore;
            TargetKey = targetKey;
            SourceResx = sourceResx;
        }

        private XDocument SourceResx { get; }

        private StringStore Store { get; }

        private string TargetKey { get; }


        private XDocument ProduceOutputXml()
        {
            var doc = new XDocument(SourceResx);
            var values = doc.XPathSelectElements("/root/data/value");

            foreach (var value in values)
            {
                var searchValue = value.Value;

                if (!Store.Has(searchValue)) continue;
                var ssi = Store[searchValue];

                if (ssi.Has(TargetKey))
                {
                    value.Value = ssi[TargetKey];
                }
            }

            return doc;
        }
    }
}