using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace VillaBisutti.Delta.Core.Data
{
	public class Usuario : DataAccessBase<Model.Usuario>
	{
		public override void Update(Model.Usuario entity)
		{
			Model.Usuario original = context.Usuario.FirstOrDefault(a => a.Id == entity.Id);
			SetUpdated(entity);
			context.Entry(original).CurrentValues.SetValues(entity);
			context.SaveChanges();
		}

		public override System.Data.Entity.Infrastructure.DbEntityEntry GetCurrent(Model.Usuario entity)
		{
			return context.Entry(entity);
		}

		public override void Insert(Model.Usuario entity)
		{
			SetCreated(entity);
			context.Usuario.Add(entity);
			context.SaveChanges();
		}

		protected override List<Model.Usuario> GetCollection()
		{
            return context.Usuario
                .Include(u => u.Perfil)
                .ToList();
		}
		//TODO: quando estiver pronto, ver o que fazer com isso
        //public List<Model.Usuario> GetPorTipo(Model.Perfil tipo)
        //{
        //    return GetCollection().Where(pf => pf.Perfis.Where(p => p == tipo).Count() > 0).ToList();
        //}


		public Model.Usuario ValidUser(Model.Usuario usuario)
		{
			Model.Usuario usuarioLogado = context.Usuario
				.Include(a => a.Perfil)
				.Include(a => a.Perfil.Modulos)
				.Include(a => a.Perfil.Modulos.Select(m => m.Modulo))
				.FirstOrDefault(a => a.Email.ToLower().Trim() == usuario.Email.ToLower().Trim() && a.Senha == usuario.Senha);
			return usuarioLogado;
		}
		public Model.Usuario EntireUser(int usuarioId)
		{
			return context.Usuario
				.Include(u => u.Perfil)
				.Include(u => u.Perfil.Modulos)
				.Include(u => u.Perfil.Modulos.Select(c => c.Modulo))
				.Where(u => u.Id == usuarioId)
				.FirstOrDefault();
		}
	}
}
