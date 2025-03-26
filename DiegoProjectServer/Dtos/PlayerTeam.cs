using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiegoProjectServer.Dtos
{
    public class PlayerTeam
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public double Points { get; set; }

        public double Rebounds { get; set; }

        public double Assists { get; set; }

        public double Minutes { get; set; }

        public string TeamName { get; set; } = null!;
    }
}
