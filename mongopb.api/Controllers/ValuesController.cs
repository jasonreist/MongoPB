using mongopb.Data;
using mongopb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace mongopb.api.Controllers
{
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        private ISessionRepo Sessionctx;

        //public ValuesController()
        //{
        //    this.context = new EntityManager();
        //}

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("GetSession/{id}")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<Session> GetSession(string id)
        {
            Sessionctx = new SessionRepo();
            return await Sessionctx.Get(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
