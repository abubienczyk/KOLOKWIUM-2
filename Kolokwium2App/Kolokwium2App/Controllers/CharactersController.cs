using System.Transactions;
using Kolokwium2App.DTO_s;
using Kolokwium2App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacersInfo(int characterId)
    {
        if (!await _dbService.DoesCharacterExist(characterId))
            return NotFound("CHARACTER NOT FOUND");
        
        var data = await _dbService.GetCharacterInfo(characterId);
        
        var result = data.Select(c => new GetCharacterInfoDTO()
        {
            FirstName = c.FirstName,
            LastName = c.LastName,
            CurrentWeight = c.CurrentWeight,
            MaxWeight = c.MaxWeight,
            BackpackItems = c.Backpacks.Select(b => new GetBackpckDTO()
            {
                ItemName = b.Items.Name,
                ItemWeight = b.Items.Weight,
                Amount = b.Amount
            }).ToList(),
            Titles = c.CharacterTitles.Select(t => new GetTitlesDTO()
            {
                Title = t.Titles.Name,
                AquiredAt = t.AcquiredAt
            }).ToList()
        });
        return Ok(result);
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddItemsToCharacter(int characterId, List<int> Ids)
    {
        if (!await _dbService.DoesCharacterExist(characterId))
            return NotFound("CHARACTER NOT FOUND");
        
        foreach (var id in Ids)
        {
            if (!await _dbService.DoesItemExist(id))
                return NotFound("NO ITEM WITH THIS ID");
        }

        var suma = await _dbService.GetItemsWeight(Ids);
        
        if (suma > await _dbService.GetCharacterFreeSpace(characterId))
            return Conflict("CHARACTER CANT CARRY ITEMS");
        
        //lista nowych rzeczy w plecaku 
        var items = new List<NewBackpackDTO>();
        foreach (var id in Ids)
        {
            var amount = await _dbService.GetItemsAmount(id, characterId)+1;
            items.Add(new NewBackpackDTO()
            {
                Amount = amount,
                ItemId = id,
                CharacterId = characterId
            });
        }
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbService.AddItemsToBackpack(items);
            await _dbService.UpdateCurrentWeight(characterId,suma);
    
            scope.Complete();
        }
        
        return Created("api/backpacks", items);
    }
}