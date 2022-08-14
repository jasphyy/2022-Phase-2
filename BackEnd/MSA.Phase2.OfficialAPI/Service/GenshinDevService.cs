
using MSA.Phase2.OfficialAPI.Models;
using Newtonsoft.Json;

namespace MSA.Phase2.OfficialAPI.Service
{
  
        public interface IGenshinDevService
        {
            Task<Character> GetGenshinCharacter(string character);

        }

        public class GenshinDevService: IGenshinDevService
        {
            private readonly HttpClient _httpClient;
        public GenshinDevService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

   

        public async Task<Character> GetGenshinCharacter(string character)
        {
            var res = await _httpClient.GetAsync($"/characters/{character}");
            var content = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Character>(content);
            return response;
        }

 
    }
    }

