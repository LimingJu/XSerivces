using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EntryPointService.Models;

namespace EntryPointService.Controllers
{
    /// <summary>
    /// This controller is only for testing purpose now
    /// </summary>
    public class ValuesController : ApiController
    {
        public string Get()
        {
            return "what do you want pls specify";
        }
        public string Get(int id)
        {
            return "value";
        }

        public IEnumerable<string> Get(string h)
        {
            return Get(h, null, null);
        }

        public IEnumerable<string> Get(string h, string w)
        {
            return Get(h, w, null);
        }

        public IEnumerable<string> Get(string h, string w, int? z)
        {
            if (z != 0)
                return new string[] { h, w, "this is z: " + z.ToString() };
            else
                return new string[] { h, w };
        }

        /// <summary>
        /// asfasdfa
        /// </summary>
        /// <param name="people">asdfsadf</param>
        /// <returns>asdfasdf</returns>
        [ResponseType(typeof(People))]
        public async Task<IHttpActionResult> Post(People people)
        {
            return Ok(people);
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
