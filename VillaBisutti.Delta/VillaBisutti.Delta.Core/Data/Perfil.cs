using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace VillaBisutti.Delta.Core.Data
{
	public class Perfil : DataAccessBase<Model.Perfil>
	{
		public override void Update(Model.Perfil entity)
		{
			SetUpdated(entity);
			List<Model.PerfilModulo> originalLocal = context.Perfil.Include(e=>e.Modulos).FirstOrDefault(s => s.Id == entity.Id).Modulos.ToList();
            foreach (var item in originalLocal)
            {
                var antigo = context.PerfilModulo.FirstOrDefault(e => e.Id == item.Id);
                var novo = entity.Modulos.FirstOrDefault(e => e.Id == item.Id);
                context.Entry(antigo).CurrentValues.SetValues(novo);
            }
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Perfil entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Perfil entity)
		{
			SetCreated(entity);
			context.Perfil.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Perfil> GetCollection()
		{
            return context.Perfil
                //.Include(m => m.Modulos.Select(c => c.Modulo))
                .ToList();
                
		}

        public Model.Perfil GetContextPerfil(int id)
        {
			return context.Perfil
				.Include(m => m.Modulos)
				.Where(m => m.Id == id)
				.FirstOrDefault();
        }

        public Model.Perfil GetPerfil(int id = 0)
        {
            return context.Perfil
                .Include(m => m.Modulos)
                .Include(m => m.Modulos.Select(c => c.Modulo))
                .SingleOrDefault(m => m.Id == id);
                
        }
    }
}
