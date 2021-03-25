using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Sample.Products.Backend.Business.Concrete.Models;

namespace Sample.Products.Backend.Business.Concrete.Services
{
    public class UnsplashClient:IUnsplashClient
    {
        private readonly HttpClient _client;

        public UnsplashClient(HttpClient client)
        {
            _client = client;
        }
        public List<UnsplashPictureModel> GetRandomPicture()
        {
            var requestMessage = new HttpRequestMessage();
            requestMessage.RequestUri = new Uri("https://api.unsplash.com/photos/random?count=30&content_filter=high");
            requestMessage.Headers.Add("Accept-Version","v1");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Client-ID","yGAEVAgbxlf7gZiEFgoBp-bMwwgaTBUsdKuAjGX7drY");
            var response=_client.SendAsync(requestMessage).ConfigureAwait(false).GetAwaiter().GetResult();
            using var content = response.Content.ReadAsStreamAsync().ConfigureAwait(true).GetAwaiter().GetResult();
            return JsonSerializer.DeserializeAsync<List<UnsplashPictureModel>>(content).ConfigureAwait(true).GetAwaiter().GetResult();
        }
    }

    public interface IUnsplashClient
    {
        List<UnsplashPictureModel> GetRandomPicture();
    }
}