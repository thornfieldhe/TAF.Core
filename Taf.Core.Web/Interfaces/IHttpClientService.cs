namespace Taf.Core.Web{
    public interface IHttpClientService{
        /// <summary>
        /// Get方法返回json数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="weatherToPackage">是否对返回结果进行解包处理</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetContentAsync<T>(string url, bool weatherToPackage = true);

        /// <summary>
        /// Post方法返回json数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="weatherToPackage">是否对返回结果进行解包处理</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> PostContentAsync<T>(string url, object data, bool weatherToPackage = true);

        Task PostAsync(string url, object data);

        Task PutAsync(string url, object data);

        /// <summary>
        /// Post方法通过表单传参返回json数据 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="weatherToPackage">是否对返回结果进行解包处理</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> PostContentFormAsync<T>(
            string url, List<KeyValuePair<string, string>> data, bool weatherToPackage = true);

        Task DeleteAsync(string url);
    }
}
