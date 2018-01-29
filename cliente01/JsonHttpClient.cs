using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cliente01
{
    class JsonHttpClient
    {
        #region GET

        public async Task<T> GetAsync<T>(Uri uri)
        {
            var responseContent = await GetAsync(uri);
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        async Task<string> GetAsync(Uri uri)
        {
            using (var client = new HttpClient())
                return await GetContentStringAsync(await client.GetAsync(uri));
        }

        #endregion

        #region POST

        public async Task<T> PostAsync<T>(Uri uri, object value, T returnAnonymousObject)
        {
            var json = JsonConvert.SerializeObject(value);
            var responseContent = await PostAsync(uri, json);

            return JsonConvert.DeserializeAnonymousType(responseContent, returnAnonymousObject);
        }

        async Task<string> PostAsync(Uri uri, string jsonContent)
        {
            using (var client = new HttpClient())
                return await GetContentStringAsync(
                    await client.PostAsync(uri, new StringContent(jsonContent, Encoding.UTF8, "application/json")));
        }

        async Task<string> GetContentStringAsync(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return responseContent;
            else
                throw new Exception($"{response.StatusCode}: {response.ReasonPhrase}\n{responseContent}");
        }

        #endregion
    }
}

