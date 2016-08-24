using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace GrafGenerator.ResxLocalize.Tests
{
    public static class Input
    {
        public static class XDoc
        {
            public static XDocument Sample = XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf-8""?>
<root>
    <resheader name=""resmimetype"">
    <value>text/microsoft-resx</value>
    </resheader>
    <resheader name=""version"">
    <value>2.0</value>
    </resheader>
    <resheader name=""reader"">
    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
    </resheader>
    <resheader name=""writer"">
    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
    </resheader>
    <data name=""presentKey"" xml:space=""preserve"">
    <value>present</value>
    </data>
    <data name=""missingKey"" xml:space=""preserve"">
    <value>missing</value>
    </data>
</root>
            ");
        }

        public static class JData
        {
            public static JArray Sample = JArray.Parse(@"[
    {
        ""ru"": ""changed_ru"",
        ""en"": ""present"",
        ""cs"": ""changed_cs""
    },
    {
        ""ru"": ""changed_ru"",
        ""en"": ""present too"",
        ""cs"": ""changed_cs""
    }
]
            ");
        }
    }
}