using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class StudentController : ApiController
{
    // GET api/<controller>

    [Route("http://app.lib.hutech.edu.vn/api/reader/RequestResetPass")]
    public IEnumerable<string> Get()
    {
        return new string[] { "cardnumber", "email" };
    }

    // GET api/<controller>/5
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<controller>
    public void Post([FromBody]string value)
    {
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }
}
