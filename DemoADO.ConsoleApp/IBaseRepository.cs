using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    public interface IBaseRepository<TKey,TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        TEntity GetOne(TKey id);
        IEnumerable<TEntity> GetAll();
        bool Update(TKey id,TEntity entity);
        bool Delete(TKey id);
    }
}
