using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model;

[Table("Player")]
public partial class Player
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public double Points { get; set; }

    public double Rebunds { get; set; }

    public double Assists { get; set; }

    public double Minutes { get; set; }

    [Column("TeamID")]
    public int TeamId { get; set; }

    [ForeignKey("TeamId")]
    [InverseProperty("Players")]
    public virtual Team Team { get; set; } = null!;
}
