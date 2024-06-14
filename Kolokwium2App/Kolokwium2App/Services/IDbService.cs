using Kolokwium2App.DTO_s;
using Kolokwium2App.Models;

namespace Kolokwium2App.Services;

public interface IDbService
{
    public Task<bool> DoesCharacterExist(int id);
    public Task<ICollection<Characters>> GetCharacterInfo(int id);
    public Task<bool> DoesItemExist(int id);
    public Task<int> GetItemsWeight(ICollection<int> Ids);
    public Task<int> GetCharacterFreeSpace(int id);
    public Task<int> GetItemsAmount(int id, int characterId);

    public Task AddItemsToBackpack(ICollection<NewBackpackDTO> data);
    public Task UpdateCurrentWeight(int id, int sum);

}