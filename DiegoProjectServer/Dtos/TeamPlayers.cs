using Microsoft.EntityFrameworkCore;
using Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiegoProjectServer.Dtos
{
    public class TeamPlayers
    {
        public int Id { get; set; }

        
        public string Name { get; set; } = null!;

        public string Conference { get; set; } = null!;

        public int Win { get; set; }

        public int Loss { get; set; }

        public List<Player>? Players { get; set; }
    }
}
