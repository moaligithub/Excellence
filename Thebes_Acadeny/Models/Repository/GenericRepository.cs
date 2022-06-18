using Microsoft.EntityFrameworkCore;
using Thebes_Acadeny.Models.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table;
        public GenericRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public void Delete(int Id)
        {
            var item = GetById(Id);
            table.Remove(item);
        }

        public IList<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int Id)
        {
            var item = table.Find(Id);
            return item;
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void UpDate(T newentity)
        {
            table.Attach(newentity);
            _context.Entry(newentity).State = EntityState.Modified;
        }
    }
}
 