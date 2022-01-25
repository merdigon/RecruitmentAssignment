using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAssignment.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<T> Get(int id);

        Task<bool> Delete(int id);

        Task<long> Create(T model);

        Task Edit(int id, T model);
    }
}
