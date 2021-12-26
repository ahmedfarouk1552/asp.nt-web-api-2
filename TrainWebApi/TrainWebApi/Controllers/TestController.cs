using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainWebApi.Models;

namespace TrainWebApi.Controllers
{
    
    public class TestController : ApiController
    {
        ITIContext db = new ITIContext();
        public IHttpActionResult get()
        {
            return Ok(db.Students.ToList());
        }
    }
}
