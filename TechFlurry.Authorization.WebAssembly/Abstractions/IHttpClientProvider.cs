using System.Net.Http;

namespace TechFlurry.Authorization.WebAssembly.Abstractions
{
    public interface IHttpClientProvider
    {
        void AddHeader(string key, string value);

        HttpClient GetHttpClient();

        void RemoveHeader(string key);
        void RemoveAllHeaders();
    }
}