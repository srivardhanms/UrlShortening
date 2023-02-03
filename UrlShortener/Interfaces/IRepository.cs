namespace UrlShortener.Interfaces
{
    public interface IRepository
    {
        void Add(string shortenedUrl, string url);
        string GetDecodedUrl(string shortenedUrl);
        string GetEncodedUrl(string url);
    }
}