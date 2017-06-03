using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using BLL.Models;

namespace API.Controllers
{
    public class UserController : ApiController
    {
		private readonly IMainService _mainService;

		public UserController(IMainService mainService)
	    {
		    _mainService = mainService;
	    }

	    public IEnumerable<Users> GetAllUsers()
		{
			return _mainService.GetAllUsers();
		}

		public UsersPageView GetUsers(int pageNumber, int pageSize)
		{
			return _mainService.GetUsersView(pageNumber, pageSize);
		}

		[HttpPost]
		public Users PostUser(Users user)
		{
			return _mainService.CreateUser(user);
		}

		[HttpDelete]
		public bool DeleteUser(int id)
		{
			return _mainService.DeleteUser(id);
		}
	}
}
