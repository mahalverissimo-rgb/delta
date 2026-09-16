using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Repositories;
using VillaBisutti.Delta.WebApp.Services.Dtos;
using VillaBisutti.Delta.WebApp.Services.Results;

namespace VillaBisutti.Delta.WebApp.Services
{
    public interface ILocalService
    {
        Task<List<Local>> GetAllAsync();
        Task<Local?> GetByIdAsync(int id);
        Task CreateAsync(Local local, int? currentUserId);
        Task<bool> UpdateAsync(Local local, int? currentUserId);
        Task<OperationResult> DeleteAsync(int id);
        Task<List<LocalResumoDto>> GetAtivosAsync();
    }

    public class LocalService : ILocalService
    {
        private readonly IRepositoryBase<Local> _locais;
        private readonly IRepositoryBase<Evento> _eventos;

        public LocalService(IRepositoryBase<Local> locais, IRepositoryBase<Evento> eventos)
        {
            _locais = locais;
            _eventos = eventos;
        }

        public Task<List<Local>> GetAllAsync() => _locais.ListAsync();

        public Task<Local?> GetByIdAsync(int id) => _locais.GetByIdAsync(id);

        public async Task CreateAsync(Local local, int? currentUserId)
        {
            local.UsuarioCreateId = currentUserId;
            local.UsuarioCreateData = DateTime.Now;

            await _locais.AddAsync(local);
            await _locais.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Local local, int? currentUserId)
        {
            local.UsuarioUpdateId = currentUserId;
            local.UsuarioUpdateData = DateTime.Now;

            try
            {
                _locais.Update(local);
                await _locais.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _locais.ExistsAsync(local.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var local = await _locais.GetByIdAsync(id);
            if (local == null)
            {
                return OperationResult.Ok();
            }

            if (await _eventos.AnyAsync(e => e.LocalId == id))
            {
                return OperationResult.Fail("Não é possível excluir este local pois existem eventos associados a ele.");
            }

            _locais.Remove(local);
            await _locais.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<List<LocalResumoDto>> GetAtivosAsync() =>
            await _locais.Query()
                .Where(l => l.Ativo)
                .Select(l => new LocalResumoDto(l.Id, l.Nome, l.Capacidade))
                .ToListAsync();
    }
}
