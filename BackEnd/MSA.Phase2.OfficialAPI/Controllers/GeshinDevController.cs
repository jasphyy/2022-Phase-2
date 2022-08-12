using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MSA.Phase2.OfficialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenshinDevController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public GenshinDevController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));

            }
            _httpClient = clientFactory.CreateClient("genshin");
        }


        /// <summary>
        /// Retrieves the characters from the Genshin API
        /// </summary>
        /// <returns>A JSON object representing the genshin characters</returns>

        [HttpGet]
        [Route("characters")]
        [ProducesResponseType(200)]

        public async Task<IActionResult> GetGenshinCharacters()
        {
            var res = await _httpClient.GetAsync("/characters");
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }



        /// <summary>
        /// Retrieves specific character from the Genshin API
        /// </summary>
        /// <param name="character">Name of the genshin character</param>
        /// <returns>A JSON object of entered character</returns>


        [HttpGet]
        [Route("character/{character}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGenshinCharacter(string character)
        {

            var res = await _httpClient.GetAsync($"/characters/{character}");
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);

        }






    }
}
