using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2App.Models;

[Table("backpacks")]
[PrimaryKey(nameof(CharacterId),nameof(ItemId))]
public class Backpacks
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }

    [ForeignKey(nameof(ItemId))] public Items Items { get; set; } = null!;
    [ForeignKey(nameof(CharacterId))] public Characters Characters { get; set; } = null!;
}