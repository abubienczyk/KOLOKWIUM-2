namespace Kolokwium2App.DTO_s;

public class GetCharacterInfoDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; } 
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<GetBackpckDTO> BackpackItems { get; set; } = new List<GetBackpckDTO>();
    public ICollection<GetTitlesDTO> Titles { get; set; } = new List<GetTitlesDTO>();
}

public class GetBackpckDTO
{
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class GetTitlesDTO
{
    public string Title { get; set; }
    public DateTime AquiredAt { get; set; }
}