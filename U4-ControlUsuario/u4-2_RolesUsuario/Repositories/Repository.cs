using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using u4_2_RolesUsuario.Models;

namespace u4_2_RolesUsuario.Repositories
{
    public class Repository<T> where T: class
    {
        public sistemadirectorContext Context { get; set; }


        public Repository(sistemadirectorContext context)
        {
            Context = context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }
  
        public virtual T GetById(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual void Insert(T entidad)
        {
            if (Validate(entidad))
            {
                Context.Add(entidad);
                Save();
            }
        }

        public virtual void Update(T entidad)
        {
            if (Validate(entidad))
            {
                Context.Update(entidad);
                Save();
            }
        }

        public virtual void Delete(T entidad)
        {
            Context.Remove(entidad);
            Save();
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public virtual bool Validate(T entidad)
        {
            //Falta hacer validaciones
            return true;
        }

    }
}
