using System;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Http;
using API.Controllers;
using Autofac;
using Autofac.Integration.WebApi;
using BLL;
using DAL;
using System.Web;

namespace API
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

			var builder = new ContainerBuilder();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			builder.RegisterType<MainService>().As<IMainService>();
			builder.RegisterType<GenericRepository>().As<IGenericRepository>();

			var container = builder.Build();

			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
			{
				HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
				HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
				HttpContext.Current.Response.End();
			}
		}
	}
}
