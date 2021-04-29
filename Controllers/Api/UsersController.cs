using FYPManagment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FYPManagment.Controllers.Api
{
    public class UsersController : ApiController
    {
        readonly ApplicationDbContext _Context;

        public UsersController()
        {
            _Context = new ApplicationDbContext();
        }

        public IHttpActionResult GetUsers(string query = null)
        {

            var UserNames = _Context.Users.Where(u => u.UserName.Contains(query)).ToList();

            return Ok(UserNames);
        }
    }
}
