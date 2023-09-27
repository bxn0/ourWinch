using System.Linq.Expressions;

namespace ourWinch.Models
{
    public interface IRepository<T> where T : class
    {
        //  T -> aktiv servise  hazir yapi
        IEnumerable<T> GetAll();    
        T GetById(Expression<Func<T, bool >>filtre);
        void Add (T entity);
        void Update (T entity);
        void Delete (T entity);

        void DeleteBetwenn(IEnumerable<T> entities);

    }
}
