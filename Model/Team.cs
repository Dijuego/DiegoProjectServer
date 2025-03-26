using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model;

[Table("Team")]
public partial class Team
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Conference { get; set; } = null!;

    public int Win { get; set; }

    public int Loss { get; set; }

    [InverseProperty("Team")]
    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
