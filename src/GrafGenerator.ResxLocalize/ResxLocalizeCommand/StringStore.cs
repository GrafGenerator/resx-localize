using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Newtonsoft.Json.Linq;

namespace GrafGenerator.ResxLocalize.ResxLocalizeCommand
{
    public class StringStore
    {
        public StringStore(JArray data, string sourceKey)
        {
            Items = (
                from jobject in data.OfType<JObject>()
                let props = jobject.Properties().ToArray()
                let propSearchValue =
                    props.FirstOrDefault(p => p.Name.Equals(sourceKey, StringComparison.InvariantCultureIgnoreCase))
                        ?.Value.ToString()
                where propSearchValue != null
                let ssi = new StringStoreItem(
                    propSearchValue,
                    props.ToDictionary(p => p.Name, p => p.Value.ToString())
                    )
                select ssi
                )
                .DistinctBy(s => s.SearchValue)
                .ToDictionary(s => s.SearchValue);
        }

        private IDictionary<string, StringStoreItem> Items { get; }

        public StringStoreItem this[string key] => Items[key];

        public bool Has(string key) => Items.ContainsKey(key);


        public class StringStoreItem
        {
            public StringStoreItem(string searchValue, IDictionary<string, string> values)
            {
                SearchValue = searchValue;
                Values = values;
            }

            public string SearchValue { get; }
            private IDictionary<string, string> Values { get; }

            public string this[string key] => Values[key];

            public bool Has(string key) => Values.ContainsKey(key);
        }
    }
}