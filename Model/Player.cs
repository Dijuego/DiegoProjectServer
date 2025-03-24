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

    public int Number { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(2, 1)")]
    public decimal Points { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal Rebunds { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal Assists { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string Minutes { get; set; } = null!;

    [Column("TeamID")]
    public int TeamId { get; set; }

    [ForeignKey("TeamId")]
    [InverseProperty("Players")]
    public virtual Team Team { get; set; } = null!;
}
