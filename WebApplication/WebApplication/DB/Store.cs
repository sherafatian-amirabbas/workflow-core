using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.DB
{
    public class Store : IStore
    {
        private readonly List<KeyValuePair<Guid, string>> collection;


        public Store()
        {
            collection = new List<KeyValuePair<Guid, string>>();
        }


        public string this[Guid key]
        {
            get
            {
                return collection.FirstOrDefault(u => u.Key == key).Value;
            }
        }

        public Guid Add(string description)
        {
            var key = Guid.NewGuid();
            collection.Add(new KeyValuePair<Guid, string>(key, description));
            return key;
        }
    }
}
