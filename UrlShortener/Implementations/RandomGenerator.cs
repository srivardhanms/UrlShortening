using System;
using UrlShortener.Interfaces;

namespace UrlShortener.Implementations
{
    public class RandomGenerator : IRandomGenerator
    {
        const string acceptedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        const int length = 7;
        public string GetRandomString()
        {
            string randomStr = string.Empty;
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                randomStr += acceptedChars[random.Next() % acceptedChars.Length];
            }

            return randomStr;
        }
    }
}
