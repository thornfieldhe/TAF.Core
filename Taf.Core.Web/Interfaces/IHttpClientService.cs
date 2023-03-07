 namespace Taf.Core.Web
{
    public interface IHttpClientService 
    {
        
        Task<T> GetContentAsync<T>(string url,bool weatherToPackage=true);

        Task<T> PostContentAsync<T>(string url, object data, bool useR =true);

        Task PostAsync(string url, object data);

        Task PutAsync(string url, object data);

        Task<T> PostContentFormAsync<T>(string url, List<KeyValuePair<string,string>> data, bool useR =true);

        Task DeleteAsync(string url);
    }
}
