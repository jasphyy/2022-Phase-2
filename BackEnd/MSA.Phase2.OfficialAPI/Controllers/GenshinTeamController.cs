using MSA.Phase2.OfficialAPI.Data;
using Microsoft.AspNetCore.Mvc;
using MSA.Phase2.OfficialAPI.Models;

namespace MSA.Phase2.OfficialAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenshinTeamController : ControllerBase
    {
        private readonly TeamAPIDbContext dbContext;
        public GenshinTeamController(TeamAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// "CREATE Operation" Add Genshin Impact characters to a team assigned to a player.
        /// </summary>
        /// <returns>a JSON object of the current Genshin Team entered.</returns>
        [HttpPost]
        public async Task<IActionResult> AddMember(AddTeamMember addMember)
        {
            var member = new Team()
            {
                Id = Guid.NewGuid(),
                FullName = addMember.FullName,
                Member1 = addMember.Member1,
                Member2 = addMember.Member2,
                Member3 = addMember.Member3,
                Member4 = addMember.Member4

            };
            await dbContext.Team.AddAsync(member);
            await dbContext.SaveChangesAsync();
            return Ok(member);
        }

        /// <summary>
        /// "READ Operation" Retrieves current Genshin Impact Teams to given player.
        /// </summary>
        /// <returns>a JSON object of the current Genshin Team of the player with a generated Id</returns>

        [HttpGet]
        public async Task<IActionResult> GetCurrentTeam()
        {
            return Ok(dbContext.Team.ToList());
        }

        /// <summary>
        /// "UPDATE Operation" Add Genshin Impact characters to a team assigned to a player.
        /// </summary>
        /// <returns>a JSON object of the updated Genshin Team entered.</returns>

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCurrentTeam([FromRoute] Guid id, UpdateTeamMember updateTeamMember )
        {
            var team = await dbContext.Team.FindAsync(id);
            if (team != null)
            {
                team.FullName = updateTeamMember.FullName;
                team.Member1 = updateTeamMember.Member1;
                team.Member2 = updateTeamMember.Member2;
                team.Member3 = updateTeamMember.Member3;
                team.Member4 = updateTeamMember.Member4;

                await dbContext.SaveChangesAsync();
                return Ok(team);
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
            var team = await dbContext.Team.FindAsync(id);
            if (team != null)
            {
                dbContext.Remove(team);

                await dbContext.SaveChangesAsync();
                return Ok("Team was deleted");
            }
            return NotFound();
           
        }
      














    }
}
