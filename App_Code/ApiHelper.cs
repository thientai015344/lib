using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
/// <summary>
/// Summary description for ApiHelper
/// </summary>
public class ApiHelper
{
    public ApiHelper()
    {
        //
        // TODO: Add constructor logic here
        //

    }
    public static async Task<T> Post<T>(string endpoint, object data)
    {
        using (var client = new HttpClient())
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(data));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //httpContent.Headers.Add("Token", "AAAAAAAAAAAAAAAAAAAAAA");

            var response = client.PostAsync(endpoint, httpContent).Result;
            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
        }
    }
}