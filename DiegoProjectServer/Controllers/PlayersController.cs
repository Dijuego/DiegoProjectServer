﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiegoProjectServer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DiegoProjectServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly NbaPlayersContext _context;

        public PlayersController(NbaPlayersContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // GET: api/Players/
        [HttpGet("PlayerTeam/{id}")]
        public async Task<ActionResult<PlayerTeam>> GetPlayerWithTeam(int id)
        {
            PlayerTeam player = await _context.Players.Include(player => player.Team).Where(player => player.Id == id)
                .Select(player =>
                new PlayerTeam
                {
                    Id = player.Id,
                    Name = player.Name,
                    Points = player.Points,
                    Rebounds = player.Rebunds,
                    Assists = player.Assists,
                    Minutes = player.Minutes,
                    TeamId = player.TeamId,
                    TeamName = player.Team.Name
                }
            ).SingleAsync();
            return player;
        }
        [HttpGet("PlayersTeam")]
        public async Task<ActionResult<IEnumerable<PlayerTeam>>> GetPlayersWithTeam()
        {
            List <PlayerTeam> players = await _context.Players.Include(player => player.Team).Select(player =>
                new PlayerTeam
                {
                    Id = player.Id,
                    Name = player.Name,
                    Points = player.Points,
                    Rebounds = player.Rebunds,
                    Assists = player.Assists,
                    Minutes = player.Minutes,
                    TeamId = player.TeamId,
                    TeamName = player.Team.Name
                }
            ).ToListAsync();
            return players;
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}
