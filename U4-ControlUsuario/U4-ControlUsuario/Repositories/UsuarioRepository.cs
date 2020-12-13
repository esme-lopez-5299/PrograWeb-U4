using System;
using System.Collections.Generic;
using System.Linq;
using U4_ControlUsuario.Models;
using System.Threading.Tasks;

namespace U4_ControlUsuario.Repositories
{
   
		public class Repository
		{
			public controlCuentasContext Context { get; set; }

			public Repository(controlCuentasContext context)
			{
				Context = context;
			}
		    			

			public Usuario GetById(object id)
			{
				return Context.Find<Usuario>(id);
			}


			public Usuario GetByCorreo(string correo)
			{
			return Context.Usuario.FirstOrDefault(x => x.CorreoElectronico.ToUpper() == correo.ToUpper());
			}

		public void Insert(Usuario entidad)
			{
				if (Validate(entidad))
				{
					Context.Add(entidad);
					Save();
				}
			}

			public void Update(Usuario entidad)
			{
				if (Validate(entidad))
				{
					Context.Update(entidad);
					Save();
				}
			}

			public void Delete(Usuario entidad)
			{

				Context.Remove(entidad);
				Save();

			}

			public void Save()
			{
				Context.SaveChanges();
			}

			public bool Validate(Usuario entidad)
			{
				return true;
			}

		}
	
}
