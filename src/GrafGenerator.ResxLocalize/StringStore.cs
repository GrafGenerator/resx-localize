using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GrafGenerator.ResxLocalize
{
    internal class StringStore
    {
        public StringStore(JArray data, string sourceKey)
        {
            Items =
                (
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
                    .Distinct(new StringStoreItemSearchValueEqualityComparer())
                    .ToDictionary(s => s.SearchValue);
        }

        private IDictionary<string, StringStoreItem> Items { get; }

        public StringStoreItem this[string key] => Items[key];

        private class StringStoreItemSearchValueEqualityComparer : EqualityComparer<StringStoreItem>
        {
            public override bool Equals(StringStoreItem x, StringStoreItem y)
            {
                return string.Equals(x.SearchValue, y.SearchValue);
            }

            public override int GetHashCode(StringStoreItem obj)
            {
                return obj?.GetHashCode() ?? 0;
            }
        }


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
        }
    }
}