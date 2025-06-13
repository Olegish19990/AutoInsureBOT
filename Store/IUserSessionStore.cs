using AutoInsureBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Store
{
    public interface IUserSessionStore
    {
        void setUserSession(long userId,UserSession userSession);
        UserSession GetUserSession(long id);
    }
}
