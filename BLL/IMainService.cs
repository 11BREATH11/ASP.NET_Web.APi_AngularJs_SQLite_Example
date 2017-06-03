using System.Collections.Generic;
using BLL.Models;

namespace BLL
{
	public interface IMainService
	{
		Users InsertUser(Users user);

		IEnumerable<Users> GetAllUsers();

		UsersPageView GetUsersView(int pageNumber, int pageSize);

		Users CreateUser(Users user);

		bool DeleteUser(int id);

		Pets InsertPets(Pets pet);

		IEnumerable<Pets> GetAllPets();

		PetsPageView GetPetsView(int userId, int pageNumber, int pageSize);

		Pets CreatePet(Pets pet);
		bool DeletePet(int id);

	}
}
