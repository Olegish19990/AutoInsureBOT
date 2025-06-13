using AutoInsureBot.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Store
{
    public class UserSessionStore : IUserSessionStore
    {
        private readonly IMemoryCache _cache;

        public UserSessionStore(IMemoryCache cache)
        {
            _cache = cache;
        }
        public UserSession GetUserSession(long id)
        {
            return _cache.Get<UserSession>(id);
        }


        public void setUserSession(long userId, UserSession userSession)
        {
            _cache.Set(userId, userSession);
        }
    }
}
