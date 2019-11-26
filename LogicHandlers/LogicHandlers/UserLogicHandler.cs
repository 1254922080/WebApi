using Entity;
using LogicHandlers.ILogicHandlers;
using Repositories.IRepositories;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlers.LogicHandlers
{
    public class UserLogicHandler : IUserLogicHandler
    {
        private readonly IUserRepositories _userRepositories;
 
        public UserLogicHandler(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public async Task<List<User>> Test_GetLists()
        {
            return await _userRepositories.Test_GetLists();
        }
    }
}
