using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IUserRepositories
    {
        Task<List<User>> Test_GetLists();
    }
}
