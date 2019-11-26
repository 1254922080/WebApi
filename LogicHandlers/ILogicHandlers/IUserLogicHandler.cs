using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicHandlers.ILogicHandlers
{
    public interface IUserLogicHandler
    {
       Task<List<User>> Test_GetLists();
    }
}
