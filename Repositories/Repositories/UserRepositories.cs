using Entity;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserRepositories : BaseRepositories<User>, IUserRepositories
    {
        private readonly TestDbContext db;
        public UserRepositories(TestDbContext context) : base(context)
        {
            db = context;
        }
        public async Task<List<User>> Test_GetLists()
        {
            //int count = Count(e => e.Id == 1);
            return await GeListAsync(e => e.Id == 1);
        }
    }
}
