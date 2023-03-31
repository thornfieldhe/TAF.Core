using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Security.Authentication;
using System.Text;
using Taf.Core.Extension;
using Taf.Core.Utility;

namespace Taf.Core.Web;

/// <summary>
/// 远程调用接口封装 
/// </summary>
public class HttpClientService : IHttpClientService, ISingletonDependency{
    private readonly IHttpClientFactory         _httpClientFactory;
    private readonly ILogger<HttpClientService> _logger;
    private readonly ILoginInfo                 _loginInfo;


    /// <summary>
    /// 
    /// </summary>
    public HttpClientService(
        IHttpClientFactory         httpClientFactory
      , ILogger<HttpClientService> logger
      , ILoginInfo                 loginInfo){
        _httpClientFactory = httpClientFactory;
        _logger            = logger;
        _loginInfo         = loginInfo;
    }

    /// <summary>
    /// Get方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<T> GetContentAsync<T>(string url, bool weatherToPackage = false){
        var       watch  = new Stopwatch();
        using var client = _httpClientFactory.CreateClient();
        var       cts    = new CancellationTokenSource(300000);
        BindDefaultHeader(client);
        watch.Start();
        _logger.LogInformation($"{new string('-', 30)}\n[接口调用]GET:{url}");
        var response = await client.GetAsync(url, cts.Token);
        var content  = await response.Content.ReadAsStringAsync(cts.Token);
        CheckHttpStatus(response, url, content);
        response.Dispose();
        watch.Stop();
        _logger.LogInformation($"[接口调用]:{url}结束,耗时:{watch.Elapsed.Milliseconds} ms\n{new string('-', 30)}");
        return GetReturnResult<T>(content, url, weatherToPackage);
    }

    private void CheckHttpStatus(HttpResponseMessage? response, string url, string? content = null){
        if(response.StatusCode == HttpStatusCode.Unauthorized){
            throw new AuthenticationException("remote error:用户未授权");
        }

        if(response.StatusCode == HttpStatusCode.InternalServerError){
            throw new ServiceUnavailableException(
                $"remote internal exception:{response.ReasonPhrase}[{response.RequestMessage}]", url
              , new Guid("1A881AB1-82F8-41E7-8AC4-62FF810AE636"));
        }

        if(response.StatusCode == HttpStatusCode.ServiceUnavailable){
            throw new ServiceUnavailableException("remote service unavailable exception:" + response.ReasonPhrase, url
                                                , new Guid("26EAFB70-3796-4FB5-96F3-CEB7FC494507"));
        }

        if(response.StatusCode != HttpStatusCode.OK){
            if(content?.Contains("success") ?? false){
                var error = JsonConvert.DeserializeObject<R>(content);
                throw new ServiceUnavailableException(error.Message, url
                                                    , new Guid("FE3809E3-7710-4BD7-9CF4-754AD4FE5F93")
                                                    , new Exception(error.Message));
            }

            throw new ServiceUnavailableException($"other remote service exception:{response.ReasonPhrase};{content}"
                                                , url, new Guid("F4867E20-4A56-4263-86F1-EF7A7830EAA6"));
        }
    }


    /// <summary>
    /// Post方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task PostAsync(string url, object data){
        var watch = new Stopwatch();
        watch.Start();
        _logger.LogInformation($"{new string('-', 30)}\n[接口调用] POST:{url},参数{JsonConvert.SerializeObject(data)}");
        using var client = _httpClientFactory.CreateClient();
        var       cts    = new CancellationTokenSource(300000);
        BindDefaultHeader(client);
        var response = await client.PostAsync(
            url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"), cts.Token);
        CheckHttpStatus(response, url);
        _logger.LogInformation($"[接口调用]:{url}结束,耗时:{watch.Elapsed.Milliseconds} ms\n{new string('-', 30)}");
        watch.Stop();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task PutAsync(string url, object data){
        var watch = new Stopwatch();
        watch.Start();
        using var client = _httpClientFactory.CreateClient();
        var       cts    = new CancellationTokenSource(300000);
        BindDefaultHeader(client);
        _logger.LogInformation($"{new string('-', 30)}\n[接口调用] PUT:{url},参数{JsonConvert.SerializeObject(data)}");
        var response = await client.PutAsync(
            url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"), cts.Token);
        CheckHttpStatus(response, url);
        _logger.LogInformation($"[接口调用]:{url}结束,耗时:{watch.Elapsed.Milliseconds} ms\n{new string('-', 30)}");
        watch.Stop();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    /// <exception cref="AuthenticationException"></exception>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task DeleteAsync(string url){
        var watch = new Stopwatch();
        watch.Start();
        using var client = _httpClientFactory.CreateClient();
        var       cts    = new CancellationTokenSource(300000);
        BindDefaultHeader(client);
        _logger.LogInformation($"{new string('-', 30)}\n[接口调用] DELETE:{url}");
        var response = await client.DeleteAsync(url, cts.Token);
        CheckHttpStatus(response, url);
        _logger.LogInformation($"[接口调用]:{url}结束,耗时:{watch.Elapsed.Milliseconds} ms\n{new string('-', 30)}");
        watch.Stop();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="weatherToPackage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="AuthenticationException"></exception>
    /// <exception cref="Exception"></exception>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<T> PostContentAsync<T>(string url, object data, bool weatherToPackage = false){
        var watch = new Stopwatch();
        watch.Start();
        using var client = _httpClientFactory.CreateClient();
        var       cts    = new CancellationTokenSource(300000);
        BindDefaultHeader(client);
        data ??= string.Empty;
        _logger.LogInformation($"{new string('-', 30)}\n[接口调用] POST:{url},参数{JsonConvert.SerializeObject(data)}");
        var response = await client.PostAsync(
            url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"), cts.Token);

        var content = await response.Content.ReadAsStringAsync(cts.Token);
        CheckHttpStatus(response, url, content);
        _logger.LogInformation($"[接口调用]:{url}结束,耗时:{watch.Elapsed.Milliseconds} ms\n{new string('-', 30)}");
        watch.Stop();
        response.Dispose();
        return GetReturnResult<T>(content, url, weatherToPackage);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="weatherToPackage"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="AuthenticationException"></exception>
    /// <exception cref="UserFriendlyException"></exception>
    public async Task<T> PostContentFormAsync<T>(
        string url, List<KeyValuePair<string, string>> data, bool weatherToPackage = false){
        var watch = new Stopwatch();
        watch.Start();
        using var client = _httpClientFactory.CreateClient();
        var       cts    = new CancellationTokenSource(300000);
        BindDefaultHeader(client);
        _logger.LogInformation($"{new string('-', 30)}\n[接口调用] POST:{url},参数{JsonConvert.SerializeObject(data)}");
        var response = await client.PostAsync(url, new FormUrlEncodedContent(data), cts.Token);

        var content = await response.Content.ReadAsStringAsync(cts.Token);
        CheckHttpStatus(response, url, content);
        _logger.LogInformation($"[接口调用]:{url}结束,耗时:{watch.Elapsed.Milliseconds} ms\n{new string('-', 30)}");
        watch.Stop();
        response.Dispose();
        return GetReturnResult<T>(content, url, weatherToPackage);
    }

    private void BindDefaultHeader(HttpClient client){
        if(!string.IsNullOrWhiteSpace(_loginInfo.LangKey)){
            client.DefaultRequestHeaders.Add("langKey"
                                           , _loginInfo.LangKey);
        }

        if(_loginInfo.Permissions is{ Count: > 0 }){
            client.DefaultRequestHeaders.Add("permissions"
                                           , string.Join(',', _loginInfo.Permissions));
        }

        if(!string.IsNullOrWhiteSpace(_loginInfo.Name)){
            client.DefaultRequestHeaders.Add("name", _loginInfo.Name);
        }

        if(_loginInfo.UserId.HasValue){
            client.DefaultRequestHeaders.Add("userId", _loginInfo.UserId.ToString());
        }


        if(!string.IsNullOrWhiteSpace(_loginInfo.Email)){
            client.DefaultRequestHeaders.Add("email", _loginInfo.Email);
        }

        if(!string.IsNullOrWhiteSpace(_loginInfo.Authorization)){
            client.DefaultRequestHeaders.Add("authorization"
                                           , _loginInfo.Authorization);
        }

        if(_loginInfo.TenantId.HasValue){
            client.DefaultRequestHeaders.Add(
                "tenantId"
              , _loginInfo.TenantId.HasValue ? _loginInfo.TenantId.ToString() : "1");
        }

        if(!string.IsNullOrWhiteSpace(_loginInfo.TraceId)){
            client.DefaultRequestHeaders.Add("traceId", $"{_loginInfo.TraceId} {Randoms.GetRandomCode(6,"0123456789abcdefghijklmnopqrstuvwxyz")}");
        }
    }


    private T GetReturnResult<T>(string content, string url, bool weatherToPackage = false){
        try{
            return !weatherToPackage
                ? JsonConvert.DeserializeObject<T>(content)
                : JsonConvert.DeserializeObject<R<T>>(content).Data;
        } catch(Exception ex){
            throw new ServiceUnavailableException($"反序列化失败,序列化字符串:{content}", url
                                                , new Guid("2972C188-8707-4D1E-89E0-F495DD5CBCBA"), ex);
        }
    }
}
