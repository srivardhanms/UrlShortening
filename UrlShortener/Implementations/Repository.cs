using System.Collections.Generic;
using System.Linq;
using UrlShortener.Interfaces;

namespace UrlShortener.Implementations
{
    public class Repository : IRepository
    {
        private Dictionary<string, string> _database = new Dictionary<string, string>();

        public void Add(string shortenedUrl, string url)
        {
            _database.Add(shortenedUrl, url);
        }

        public string GetDecodedUrl(string shortenedUrl)
        {
            return _database.GetValueOrDefault(shortenedUrl);
        }

        public string GetEncodedUrl(string url)
        {
            return _database.FirstOrDefault(u => u.Value.Equals(url)).Key;
        }
    }
}
