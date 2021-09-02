using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Services
{
    public class WebService
    {
        public async Task<List<Album>> GetAlbums(int userId = 0)
        {
            var client = new HttpClient();
            var request = await client.GetAsync(userId == 0 ? $"http://jsonplaceholder.typicode.com/albums" : $"http://jsonplaceholder.typicode.com/albums/?userId={userId}");
            var stringResponse = await request.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<Album>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return result;
        }

        public async Task<List<Photo>> GetAllPhotos()
        {
            var client = new HttpClient();
            var request = await client.GetAsync($"http://jsonplaceholder.typicode.com/photos");
            var stringResponse = await request.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<Photo>>(stringResponse,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return result;
        }

        //public async Task<List<Photo>> GetPhoto(int id = 0)
        //{
        //    var client = new HttpClient();
        //    var request = await client.GetAsync($"http://jsonplaceholder.typicode.com/photos/?id={id}");
        //    var stringResponse = await request.Content.ReadAsStringAsync();
        //    var result = JsonSerializer.Deserialize<List<Photo>>(stringResponse,
        //        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        //    return result;
        //}
    }
}
