using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace TokenTools.Utils
{
    public class QueryParameterCollection : NameObjectCollectionBase
    {
        public QueryParameterCollection()
            : base() { }

        public QueryParameterCollection(string queryString)
            : base()
        {
            var dict = System.Web.HttpUtility.ParseQueryString(queryString);
            for (int i = 0; i < dict.Count; i++)
            {
                this.Add(dict.GetKey(i), string.Join("", dict.GetValues(i)));
            }
        }

        /// <summary>
        /// Gets all the keys in the QueryParameterCollection
        /// </summary>
        public String[] AllKeys { get { return BaseGetAllKeys(); } }

        /// <summary>
        /// Represents the entry with the specified key in the QueryParameterCollection
        /// </summary>
        public String this[String name] { get { return Get(name); } set { Set(name, value); } }

        /// <summary>
        /// Adds an entry with the specified name and value to the QueryParameterCollection
        /// </summary>
        public void Add(String name, String value)
        {
            // remove any url encoding
            name = HttpUtility.UrlDecode(name);
            value = HttpUtility.UrlDecode(value);

            // remove any existing keys
            if (BaseGetAllKeys().Contains("name"))
                BaseRemove(name);

            // add key and value
            BaseAdd(name, value);
        }

        /// <summary>
        /// Clear the QueryParameterCollection of values
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// Gets the value of the specified key from the QueryParameterCollection
        /// </summary>
        public String Get(String name)
        {
            return (string)this.BaseGet(name);
        }

        /// <summary>
        /// Returns true if the QueryParameterCollection contains one or more keys
        /// </summary>
        public bool HasKeys()
        {
            return BaseHasKeys();
        }

        /// <summary>
        /// Removes an key from the QueryParameterCollection
        /// </summary>
        /// <param name="name"></param>
        public void Remove(String name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// Adds a value to an entry in the QueryParameterCollection
        /// </summary>
        public void Set(String name, String value)
        {
            ArrayList values = new ArrayList(1);
            values.Add(value);
            BaseSet(name, values);
        }

        /// <summary>
        /// Return the QueryParameterCollection as a "&" delimited string
        /// </summary>
        public String ToQueryString(bool includePrefix = false)
        {
            if (includePrefix)
                return "?" + GetAsDelimitedString();
            else
                return GetAsDelimitedString();

        }

        public String ToDelimitedString(string delimiter)
        {
            return GetAsDelimitedString(delimiter, false);
        }

        /// <summary>
        /// Return the QueryParameterCollection as a "&" delimited string (without leading "?")
        /// </summary>
        public new String ToString()
        {
            return GetAsDelimitedString();
        }

        /// <summary>
        /// Return the QueryParameterCollection as a Dicsionary
        /// </summary>
        public Dictionary<string, string> ToDictionary()
        {
            //return this.Cast<string>().ToDictionary(k => k, v => this[v]);
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var key in BaseGetAllKeys())
            {
                var value = this[key];
                if (value != null)
                    dictionary.Add(key, value);
            }
            return dictionary;
        }

        /// <summary>
        /// Return the QueryParameterCollection as a JSON object
        /// </summary>
        public String ToJson()
        {
            return JsonConvert.SerializeObject(this.ToDictionary());
        }

        /// <summary>
        /// Return the QueryParameterCollection as FormUrlEncodedContent for use in HTTP POST body
        /// </summary>
        public FormUrlEncodedContent ToFormUrlEncodedContent()
        {
            return new FormUrlEncodedContent(this.ToDictionary());
        }

        /// <summary>
        /// Compair the current set of Keys against a set of required Keys
        /// </summary>
        public bool ValidateKeys(List<string> requiredKeys)
        {
            return ValidateQueryKeys(requiredKeys.ToArray());
        }

        /// <summary>
        /// Compair the current set of Keys against a set of required Keys
        /// </summary>
        public bool ValidateKeys(String[] requiredKeys)
        {
            return ValidateQueryKeys(requiredKeys);
        }

        private String GetAsDelimitedString(string delimiter = "&", bool urlEncode = true)
        {
            // If we don't have any entries, just bug out
            if (this.Count == 0) return string.Empty;

            ArrayList paramSets = new ArrayList();
            foreach (var key in BaseGetAllKeys())
            {
                var value = this[key];
                if (value != null)
                {
                    // Url Encode the components
                    string encodedKey = (urlEncode) ? HttpUtility.UrlEncode(key) : key;
                    string encodedValue = (urlEncode) ? HttpUtility.UrlEncode(value) : value;

                    // Construct paramSet
                    string param = $"{encodedKey}={encodedValue}";


                    // Add it to the param array
                    paramSets.Add(param);
                }
            }

            string result = String.Join(delimiter, paramSets.ToArray());
            return result;
        }

        private bool ValidateQueryKeys(string[] requiredKeys)
        {
            var queryKeys = BaseGetAllKeys();
            foreach (var key in requiredKeys)
            {
                if (!queryKeys.Contains(key)) return false;
            }
            return true;
        }
    }
}
