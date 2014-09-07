using Autofac;
using Autofac.Integration.Mvc;
using MyApp.Interfaces;
using MyApp.Services;
using System.Reflection;
using System.Web.Mvc;

namespace MyApp
{
	/// <summary>
	/// DI Container for constructor injection	
	/// </summary>
	public class Bootstrap
	{
		public IContainer Container { get; private set; }

		public void Configure()
		{
			var builder = new ContainerBuilder();
			OnConfigure(builder);

			if (this.Container == null) this.Container = builder.Build();
			else builder.Update(this.Container);

			DependencyResolver.SetResolver(new AutofacDependencyResolver(this.Container));
		}

		protected virtual void OnConfigure(ContainerBuilder builder)
		{
			builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DataService>().As<IDataService>();
            builder.RegisterType<ReportService>().As<IReportService>();		
		}
	}
}