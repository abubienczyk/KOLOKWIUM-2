using Kolokwium2App.Data;
using Kolokwium2App.DTO_s;
using Kolokwium2App.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2App.Services;

public class DbService : IDbService
{
    private readonly MyContext _context;

    public DbService(MyContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesCharacterExist(int id)
    {
        return await _context.Characters.AnyAsync(c => c.Id == id);
    }

    public async Task<ICollection<Characters>> GetCharacterInfo(int id)
    {
        return await _context.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(b => b.Items)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Titles)
            .Where(c => c.Id == id)
            .ToListAsync();
    }

    public async Task<bool> DoesItemExist(int id)
    {
        return await _context.Items.AnyAsync(i => i.Id == id);
    }

    public async Task<int> GetItemsWeight(ICollection<int> Ids)
    {
        int suma = 0;
        foreach (var id in Ids)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
            suma += item.Weight;
        }

        return suma;
    }

    public async  Task<int> GetCharacterFreeSpace(int id)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        return character.MaxWeight - character.CurrentWeight;
    }

    public async Task<int> GetItemsAmount(int id, int characterId)
    {
        var item = await _context.Backpacks.FirstOrDefaultAsync(b=> b.ItemId == id && b.CharacterId==characterId);
        return item != null ? item.Amount : 0;
    }

    public async Task AddItemsToBackpack(ICollection<NewBackpackDTO> data)
    {
        foreach (var item in data)
        {
            var backpack =
                await _context.Backpacks.FirstOrDefaultAsync(b =>
                    b.ItemId == item.ItemId && b.CharacterId == item.CharacterId);
            //jest juz taki przedmiot 
            if (backpack != null)
            {
                backpack.Amount += 1;
                _context.Backpacks.Update(backpack);
                await  _context.SaveChangesAsync();
            }
            //nie ma, trzeba dodac nowy plecak 
            else
            {
                backpack = new Backpacks()
                {
                    CharacterId = item.CharacterId,
                    ItemId = item.ItemId,
                    Amount = 1
                };
                await _context.Backpacks.AddAsync(backpack);
                await  _context.SaveChangesAsync();
            }
        }
        
    }

    public async Task UpdateCurrentWeight(int id, int sum)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
         character.CurrentWeight += sum;
         
        _context.Characters.Update(character);
        await _context.SaveChangesAsync();
    }
}