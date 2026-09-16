using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Repositories;

namespace VillaBisutti.Delta.WebApp.Services
{
    public interface IEventoService
    {
        Task<List<Evento>> GetAllAsync();
        Task<Evento?> GetDetailsAsync(int id);
        Task<Evento?> GetByIdAsync(int id);
        Task<Evento?> GetWithLocalCardapioAsync(int id);
        Task CreateAsync(Evento evento, int? currentUserId);
        Task<bool> UpdateAsync(Evento evento, int? currentUserId);
        Task DeleteAsync(int id);
    }

    public class EventoService : IEventoService
    {
        private readonly IRepositoryBase<Evento> _eventos;

        public EventoService(IRepositoryBase<Evento> eventos)
        {
            _eventos = eventos;
        }

        public Task<List<Evento>> GetAllAsync() =>
            _eventos.ListAsync(q => q
                .Include(e => e.Local)
                .Include(e => e.Cardapio)
                .Include(e => e.TipoServico)
                .Include(e => e.Produtora)
                .Include(e => e.PosVendedora));

        public Task<Evento?> GetDetailsAsync(int id) =>
            _eventos.GetByIdAsync(id, q => q
                .Include(e => e.Local)
                .Include(e => e.Cardapio)
                .Include(e => e.TipoServico)
                .Include(e => e.Produtora)
                .Include(e => e.PosVendedora));

        public Task<Evento?> GetByIdAsync(int id) => _eventos.GetByIdAsync(id);

        public Task<Evento?> GetWithLocalCardapioAsync(int id) =>
            _eventos.GetByIdAsync(id, q => q
                .Include(e => e.Local)
                .Include(e => e.Cardapio));

        public async Task CreateAsync(Evento evento, int? currentUserId)
        {
            evento.UsuarioCreateId = currentUserId;
            evento.UsuarioCreateData = DateTime.Now;

            await _eventos.AddAsync(evento);
            await _eventos.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Evento evento, int? currentUserId)
        {
            evento.UsuarioUpdateId = currentUserId;
            evento.UsuarioUpdateData = DateTime.Now;

            try
            {
                _eventos.Update(evento);
                await _eventos.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _eventos.ExistsAsync(evento.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var evento = await _eventos.GetByIdAsync(id);
            if (evento != null)
            {
                _eventos.Remove(evento);
                await _eventos.SaveChangesAsync();
            }
        }
    }
}
