using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using SQLite;

namespace DAL
{
	public class GenericRepository : IGenericRepository
	{
		#region Context Property
		SQLiteConnection _context;
		protected SQLiteConnection Context
		{
			get
			{
				return _context;
			}
			set
			{
				_context = value;
			}
		}
		#endregion

		#region Constructor
		public GenericRepository()
		{
			var codeBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
			var uri = new UriBuilder(codeBase);
			var path = Uri.UnescapeDataString(uri.Path);

			Context = new SQLiteConnection(path + "\\" + ConfigurationManager.AppSettings["DBPath"]);

			Context.Execute("PRAGMA foreign_keys=ON");
		}
		#endregion

		#region Generic Repository
		public T Insert<T>(T model)
		{
			int iRes = Context.Insert(model);
			return model;
		}

		public T Update<T>(T model)
		{
			int iRes = Context.Update(model);
			return model;
		}

		public bool Delete<T>(T model)
		{
			int iRes = Context.Delete(model);
			return iRes.Equals(1);
		}

		public T Select<T>(int pk) where T : new()
		{
			var map = Context.GetMapping(typeof(T));

			return Context.Query<T>(map.GetByPrimaryKeySql, pk).First();
		}

		public TableQuery<T> Table<T>() where T : new()
		{
			return Context.Table<T>();
		}

		public T[] SelectAll<T>() where T : new()
		{
			return new TableQuery<T>(Context).ToArray();
		}

		#endregion

		#region IDispose Region
		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this._disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

	}
}
