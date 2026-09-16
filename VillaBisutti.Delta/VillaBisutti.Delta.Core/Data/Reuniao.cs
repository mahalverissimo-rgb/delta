using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class Reuniao : DataAccessBase<Model.Reuniao>
	{
		public override void Update(Model.Reuniao entity)
		{
			SetUpdated(entity);
			Model.Reuniao reuniao = context.Reuniao.FirstOrDefault(s => s.Id.Equals(entity.Id));
			context.Entry(reuniao).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Reuniao entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Reuniao entity)
		{
			SetCreated(entity);
			context.Reuniao.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Reuniao> GetCollection()
		{
			return context.Reuniao.Include(r => r.TipoReuniao).Include(r => r.Usuario).ToList();
		}
		public List<Model.Reuniao> ReunioesEvento(int eventoId)
		{
			return GetCollection().Where(r => r.EventoId == eventoId).ToList();
		}
		public List<Model.Reuniao> ReunioesUsuario(int usuarioId)
		{
			return context.Reuniao
				.Include(r => r.Evento)
				.Include(r => r.Evento.TipoEvento)
				.Include(r => r.Evento.Local)
				.Include(r => r.TipoReuniao)
				.Include(r => r.Usuario)
				.Where(r => r.UsuarioId == usuarioId).ToList();
		}

        public List<Model.Reuniao> Filtrar(DateTime inicio, DateTime fim, bool? realizada, int? tipoReuniaoId, string filtro, int? usuarioId)
        {
            IEnumerable <Model.Reuniao> reunioes = context.Reuniao
                 .Include(a => a.Usuario)
                 .Include(a => a.Evento)
                 .Include(a => a.TipoReuniao)
                 .Include(a => a.Evento.Local)
                 .Where(x =>
                     x.Data.CompareTo(inicio) >= 0
                     && x.Data.CompareTo(fim) <= 0
                     && (x.Executada == realizada.Value || !realizada.HasValue)
					 && (x.TipoReuniaoId == tipoReuniaoId.Value || !tipoReuniaoId.HasValue)
					 && (x.UsuarioId == usuarioId.Value || !usuarioId.HasValue)
                     && (x.Evento.NomeHomenageados.IndexOf(filtro.ToLower()) >= 0
						|| x.Evento.CPFResponsavel.IndexOf(filtro.ToLower()) >= 0
						|| x.Evento.EmailContato.IndexOf(filtro.ToLower()) >= 0
						|| x.Evento.TelefoneContato.IndexOf(filtro.ToLower()) >= 0
						|| x.Evento.Local.NomeCasa.IndexOf(filtro.ToLower()) >= 0
						|| string.IsNullOrEmpty(filtro)
					 )
					 ).Take(200);
            return reunioes.ToList();
        }
    }
}
