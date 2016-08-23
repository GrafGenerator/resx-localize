using System;
using System.Xml.Linq;

namespace GrafGenerator.ResxLocalize
{
    internal partial class ResxLocalizer
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
            throw new NotImplementedException();
        }
    }
}