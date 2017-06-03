using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DAL
{
	public interface IGenericRepository : IDisposable
	{
		T Insert<T>(T model);
		T Update<T>(T model);
		bool Delete<T>(T model);
		T Select<T>(int pk) where T : new();
		T[] SelectAll<T>() where T : new();
		TableQuery<T> Table<T>() where T : new();
	}
}
