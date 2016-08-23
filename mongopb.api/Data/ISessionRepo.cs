using mongopb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mongopb.Data
{
    interface ISessionRepo
    {
        Task<List<Session>> Get();
        Task<Models.Session> Get(string id);
        Session Post(Session item);
        bool Delete(string id);
        bool Put(Session item);
    }
}
