using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindDB2021.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        T GetById(int? id);
        Task<T> GetByIdAsync(int? id);

        bool Insert(T obj);
        Task<bool> InsertAsync(T obj);

        bool Update(T obj);
        Task<bool> UpdateAsync(T obj);

        bool Delete(T obj);
        Task<bool> DeleteAsync(T obj);
    }
}
