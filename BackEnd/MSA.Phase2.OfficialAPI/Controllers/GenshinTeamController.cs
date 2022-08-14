using MSA.Phase2.OfficialAPI.Data;
using Microsoft.AspNetCore.Mvc;
using MSA.Phase2.OfficialAPI.Models;
using MSA.Phase2.OfficialAPI.Service;
using Newtonsoft.Json;
using System.Net.Http;

namespace MSA.Phase2.OfficialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenshinTeamController : ControllerBase
    {
        private readonly TeamAPIDbContext dbContext;
        private readonly IGenshinDevService _genshinService;
        public GenshinTeamController(IGenshinDevService genshinService, TeamAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
            _genshinService = genshinService;

        }

        /// <summary>
        /// "CREATE Operation" Assigns a Genshin Impact character to a player.
        /// </summary>
        /// <returns>a JSON object of the current Genshin party.</returns>
        [HttpPost]
        public async Task<IActionResult> AddParty(AddPartyMember addMember)
        {
            Character response = await _genshinService.GetGenshinCharacter(addMember.Name);
          

            var party = new Party()
            {
                Id = Guid.NewGuid(),
                FullName = addMember.FullName,
                Name = addMember.Name,
                Description = response.Description,
                Nation = response.Nation,
                Vision = response.Vision,
                Weapon = response.Weapon
            };
            await dbContext.Party.AddAsync(party);
            await dbContext.SaveChangesAsync();
            return Ok(party);
        }

        /// <summary>
        /// "READ Operation" Retrieves current Genshin Impact characters and information assigned to the identified player.
        /// </summary>
        /// <returns>a JSON object of the player's Genshin character and their information.</returns>

        [HttpGet]
        public async Task<IActionResult> GetCurrentParty()
        {
            return Ok(dbContext.Party.ToList());
        }

        /// <summary>
        /// "UPDATE Operation" Change the player's name or their assigned character.
        /// </summary>
        /// <returns>a JSON object of the updated genshin character.</returns>

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCurrentTeam([FromRoute] Guid id, UpdatePartyMember updateTeamMember )
        {
            var party = await dbContext.Party.FindAsync(id);
            Character response = await _genshinService.GetGenshinCharacter(updateTeamMember.Name);

            if (party != null)
            {
                party.FullName = updateTeamMember.FullName;
                party.Name = updateTeamMember.Name;
                party.Vision = response.Vision;
                party.Weapon = response.Weapon;
                party.Nation = response.Nation;
                party.Description = response.Description;

                await dbContext.SaveChangesAsync();
                return Ok(party);
            }
            return NotFound();

        }

        /// <summary>
        /// "DELETE Operation" Deletes Genshin Impact team
        /// </summary>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] Guid id)
        {
            var party = await dbContext.Party.FindAsync(id);
            if (party != null)
            {
                dbContext.Remove(party);

                await dbContext.SaveChangesAsync();
                return Ok("Team was deleted");
            }
            return NotFound();
           
        }

        /// <summary>
        /// "External API Call" Retrieves specific character from the Genshin API, retrieving their name, vision, weapon, nation and description
        /// </summary>
        /// <param name="character">Name of the genshin character</param>
        /// <returns>A JSON object of the genshin character</returns>

        [HttpGet]
        [Route("character/{character}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<Character> GetGenshinCharacter(string character)
        {
            return await _genshinService.GetGenshinCharacter(character);
        }














    }
}
