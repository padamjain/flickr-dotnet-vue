using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace planday.Services.ImagesService
{
  public class ImageProviderService
  {
    const string KEY = "6007eeacd4590c7fd7848c90128943e3";
    const int COUNT = 30;
    const string GET_METHOD = "flickr.photos.getRecent";
    const string SEARCH_METHOD = "flickr.photos.search";
    const string FORMAT = "json";

    public async Task<string> Search(string tags)
    {
      var httpClient = HttpClientFactory.Create();
      var searhtags_temp = tags.Split(new char[] { ',', ';', ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
      var searhtags = String.Join(",", searhtags_temp);
      var data = "";
      var url = "";

      if (searhtags.Length > 0)
      {
        url = $"https://www.flickr.com/services/rest/?api_key={KEY}&method={SEARCH_METHOD}&format={FORMAT}&tags={searhtags}&per_page={COUNT}";
      }
      else
      {
        url = $"https://www.flickr.com/services/rest/?api_key={KEY}&method={GET_METHOD}&format={FORMAT}&per_page={COUNT}";
      }

      HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);
      if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
      {
        var content = httpResponseMessage.Content;
        data = await content.ReadAsStringAsync();
      }
      else
      {
        Console.WriteLine($"Error: " + httpResponseMessage.StatusCode);
      }

      return data;
    }
  }
}