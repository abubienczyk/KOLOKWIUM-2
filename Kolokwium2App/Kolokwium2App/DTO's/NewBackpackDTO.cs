using System.ComponentModel.DataAnnotations;

namespace Kolokwium2App.DTO_s;

public class NewBackpackDTO
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Amount { get; set; }
    [Required]
    public int ItemId { get; set; }
    [Required]
    public int CharacterId { get; set; }
}