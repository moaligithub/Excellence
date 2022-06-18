using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public interface IUnitOfWork<T> where T:class
    {
        IGenericRepository<T> Entity { get; }
        void Save();
    }
}
