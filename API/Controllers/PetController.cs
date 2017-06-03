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
    public class PetController : ApiController
    {
		private readonly IMainService _mainService;

		public PetController(IMainService mainService)
	    {
		    _mainService = mainService;
	    }

		public IEnumerable<Pets> GetAllpets()
		{
			return _mainService.GetAllPets();
		}

		[HttpGet]
		public PetsPageView GetPets(int userId,int pageNumber, int pageSize)
		{
			return _mainService.GetPetsView(userId,pageNumber, pageSize);
		}

		[HttpPost]
		public Pets PostPet(Pets pet)
		{
			return _mainService.CreatePet(pet);
		}

		[HttpDelete]
		public bool DeletePet(int id)
		{
			return _mainService.DeletePet(id);
		}
	}
}
