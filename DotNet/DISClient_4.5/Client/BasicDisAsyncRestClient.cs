using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace DISClient_4._5.Client
{
    public class BasicDisAsyncRestClient : IBasicIdisAsyncRestClient
    {
        public async Task<T> PostAsync<T>(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            string kkk = await ExecutePostAsync(baseUrl, resource, headerMaps, req);

            return (T)Convert.ChangeType(kkk, TypeCode.String);
        }

        public async Task<String> ExecutePostAsync(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            var client = new RestSharp.RestClient(baseUrl);
            var request = new RestRequest(Method.POST) { Resource = resource.Trim('/') };
            var tmpJson = JsonConvert.SerializeObject(req);
            request.AddParameter("application/json; charset=utf-8", tmpJson, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            foreach (KeyValuePair<string, string> keyValuePair in headerMaps)
            {
                request.AddHeader(keyValuePair.Key, keyValuePair.Value);
            }

            string responseResult;
            string errorMessage;
            int statusCode;
            string result;

            var task = new TaskCompletionSource<string>();
            try
            {
                var response = client.ExecuteAsync(request, (p) =>
                 {
                     responseResult = p.Content;
                     statusCode = (int)p.StatusCode;
                     errorMessage = p.ErrorMessage;
                     if (statusCode >= 200 && statusCode < 300)
                     {
                         result = !string.IsNullOrEmpty(responseResult)
                            ? responseResult.Equals("{}") ? statusCode.ToString() + "\n" : statusCode.ToString() + "\n" + responseResult
                            : statusCode.ToString() + "\n";
                         task.SetResult(result);
                         
                     }
                     else
                     {
                         if (!string.IsNullOrEmpty(responseResult))
                         {
                             result = statusCode.ToString() + "\n" + responseResult;
                             task.SetResult(result);
                         }
                         else
                         {
                             result = statusCode.ToString() + "\n" + errorMessage;
                             task.SetResult(result);
                         }
                     }
                 });
                return await task.Task;
            }
            catch (Exception e)
            {
                task.SetResult(e.Message);
                return await task.Task;
            }
        }
    }
}
