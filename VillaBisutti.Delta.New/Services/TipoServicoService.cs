using Microsoft.EntityFrameworkCore;
using VillaBisutti.Delta.WebApp.Models;
using VillaBisutti.Delta.WebApp.Repositories;
using VillaBisutti.Delta.WebApp.Services.Dtos;
using VillaBisutti.Delta.WebApp.Services.Results;

namespace VillaBisutti.Delta.WebApp.Services
{
    public interface ITipoServicoService
    {
        Task<List<TipoServico>> GetAllAsync();
        Task<TipoServico?> GetByIdAsync(int id);
        Task CreateAsync(TipoServico tipoServico, int? currentUserId);
        Task<bool> UpdateAsync(TipoServico tipoServico, int? currentUserId);
        Task<OperationResult> DeleteAsync(int id);
        Task<List<TipoServicoResumoDto>> GetAtivosAsync();
    }

    public class TipoServicoService : ITipoServicoService
    {
        private readonly IRepositoryBase<TipoServico> _tiposServico;
        private readonly IRepositoryBase<Evento> _eventos;

        public TipoServicoService(IRepositoryBase<TipoServico> tiposServico, IRepositoryBase<Evento> eventos)
        {
            _tiposServico = tiposServico;
            _eventos = eventos;
        }

        public Task<List<TipoServico>> GetAllAsync() => _tiposServico.ListAsync();

        public Task<TipoServico?> GetByIdAsync(int id) => _tiposServico.GetByIdAsync(id);

        public async Task CreateAsync(TipoServico tipoServico, int? currentUserId)
        {
            tipoServico.UsuarioCreateId = currentUserId;
            tipoServico.UsuarioCreateData = DateTime.Now;

            await _tiposServico.AddAsync(tipoServico);
            await _tiposServico.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(TipoServico tipoServico, int? currentUserId)
        {
            tipoServico.UsuarioUpdateId = currentUserId;
            tipoServico.UsuarioUpdateData = DateTime.Now;

            try
            {
                _tiposServico.Update(tipoServico);
                await _tiposServico.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _tiposServico.ExistsAsync(tipoServico.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<OperationResult> DeleteAsync(int id)
        {
            var tipoServico = await _tiposServico.GetByIdAsync(id);
            if (tipoServico == null)
            {
                return OperationResult.Ok();
            }

            if (await _eventos.AnyAsync(e => e.TipoServicoId == id))
            {
                return OperationResult.Fail("Não é possível excluir este tipo de serviço pois existem eventos associados a ele.");
            }

            _tiposServico.Remove(tipoServico);
            await _tiposServico.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<List<TipoServicoResumoDto>> GetAtivosAsync() =>
            await _tiposServico.Query()
                .Where(t => t.Ativo)
                .Select(t => new TipoServicoResumoDto(t.Id, t.Nome))
                .ToListAsync();
    }
}
