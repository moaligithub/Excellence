using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public interface IGenericRepository<T> where T:class
    {
        IList<T> GetAll();
        T GetById(int Id);
        void Insert(T entity);
        void Delete(int Id);
        void UpDate(T newentity);
    }
}
