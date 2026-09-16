using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Repositories;
using VillaBisutti.Delta.WebApp.Services.Dtos;
using VillaBisutti.Delta.WebApp.Services.Results;

namespace VillaBisutti.Delta.WebApp.Services
{
    public interface ICardapioService
    {
        Task<List<Cardapio>> GetAllAsync();
        Task<Cardapio?> GetDetailsAsync(int id);
        Task CreateAsync(Cardapio cardapio, int? currentUserId);
        Task<bool> UpdateAsync(Cardapio cardapio, int? currentUserId);
        Task<OperationResult> DeleteAsync(int id);
        Task<List<CardapioResumoDto>> GetAtivosAsync();
    }

    public class CardapioService : ICardapioService
    {
        private readonly IRepositoryBase<Cardapio> _cardapios;
        private readonly IRepositoryBase<Evento> _eventos;

        public CardapioService(IRepositoryBase<Cardapio> cardapios, IRepositoryBase<Evento> eventos)
        {
            _cardapios = cardapios;
            _eventos = eventos;
        }

        public Task<List<Cardapio>> GetAllAsync() =>
            _cardapios.ListAsync(q => q.Include(c => c.Pratos));

        public Task<Cardapio?> GetDetailsAsync(int id) =>
            _cardapios.GetByIdAsync(id, q => q.Include(c => c.Pratos));

        public async Task CreateAsync(Cardapio cardapio, int? currentUserId)
        {
            cardapio.UsuarioCreateId = currentUserId;
            cardapio.UsuarioCreateData = DateTime.Now;

            await _cardapios.AddAsync(cardapio);
            await _cardapios.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Cardapio cardapio, int? currentUserId)
        {
            cardapio.UsuarioUpdateId = currentUserId;
            cardapio.UsuarioUpdateData = DateTime.Now;

            try
            {
                _cardapios.Update(cardapio);
                await _cardapios.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _cardapios.ExistsAsync(cardapio.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var cardapio = await _cardapios.GetByIdAsync(id);
            if (cardapio == null)
            {
                return OperationResult.Ok();
            }

            if (await _eventos.AnyAsync(e => e.CardapioId == id))
            {
                return OperationResult.Fail("Não é possível excluir este cardápio pois existem eventos associados a ele.");
            }

            _cardapios.Remove(cardapio);
            await _cardapios.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<List<CardapioResumoDto>> GetAtivosAsync() =>
            await _cardapios.Query()
                .Where(c => c.Ativo)
                .Select(c => new CardapioResumoDto(c.Id, c.Nome, c.PrecoPorPessoa))
                .ToListAsync();
    }
}
