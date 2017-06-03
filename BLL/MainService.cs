using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using BLL.Models;
using DAL;

namespace BLL
{
	public class MainService: IMainService
	{
		private readonly IGenericRepository _gRep;
		
		public MainService(IGenericRepository genericRepository)
		{
			_gRep = genericRepository;
		}

		#region Users

		public Users InsertUser(Users user)
		{
			var newUser = _gRep.Insert<Users>(user);

			return newUser;
		}

		public IEnumerable<Users> GetAllUsers()
		{
			return _gRep.SelectAll<Users>();
		}

		public UsersPageView GetUsersView(int pageNumber, int pageSize)
		{
			var usersView = from user in _gRep.Table<Users>().Skip((pageNumber - 1) * pageSize).Take(pageSize)
				join pet in _gRep.Table<UsersPets>()
				on user.Id equals pet.UserId into t
				group t by new {user.Id, user.Name } into grouped
				select new UsersView
				{
					Id = grouped.Key.Id,
					Name = grouped.Key.Name,
					TotalPets = grouped.First().Count()
				};
						
			var usersPageView = new UsersPageView
			{
				Items = usersView,
				TotalItems = _gRep.Table<Users>().Count()
			};

			return usersPageView;
		}

		public Users CreateUser(Users user)
		{
			return _gRep.Insert(user);
		}
		public bool DeleteUser(int id)
		{
			var user = _gRep.Select<Users>(id);
			return _gRep.Delete(user);
		}

		#endregion

		#region Pets

		public Pets InsertPets(Pets pet)
		{
			var newPet = _gRep.Insert<Pets>(pet);

			return newPet;
		}

		public IEnumerable<Pets> GetAllPets()
		{
			return _gRep.SelectAll<Pets>();
		}

		public PetsPageView GetPetsView(int userId, int pageNumber, int pageSize)
		{
			var petsView = from pets in _gRep.Table<Pets>()
							from userPet in _gRep.Table<UsersPets>().Where(p => p.UserId == userId)
							where userPet.PetId == pets.Id
							select pets;

			var enumerable = petsView as IList<Pets> ?? petsView.ToList();
			var petsPageView = new PetsPageView
			{
				Items = enumerable.Skip((pageNumber - 1) * pageSize).Take(pageSize),
				TotalItems = enumerable.Count(),
				UserName = _gRep.Table<Users>().First(p => p.Id == userId).Name

			};

			return petsPageView;
		}

		public Pets CreatePet(Pets pet)
		{
			var newPet = _gRep.Insert(pet);

			var petUser = new UsersPets
			{
				UserId = pet.UserId,
				PetId = newPet.Id
			};

			_gRep.Insert(petUser);

			return newPet;
		}
		public bool DeletePet(int id)
		{
			var user = _gRep.Select<Pets>(id);
			return _gRep.Delete(user);
		}

		#endregion
	}
}
