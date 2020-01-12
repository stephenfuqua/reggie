using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using Ninject;
using Reggie.UI.ViewModels;
using safnet.SystemAdapters;

namespace Reggie
{
	/// <summary>
	/// Setup Caliburn and Ninject for IoC.
	/// </summary>
	public class AppBootstrapper : Bootstrapper<IReggieBasic>
	{
		private IKernel m_kernel;


		protected override void Configure()
		{
			m_kernel = new StandardKernel();

			m_kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
			m_kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

			m_kernel.Bind<Reggie.UI.Utility.IHelperFactory>().To<Reggie.UI.Utility.ReggieHelperFactory>().InSingletonScope();
			m_kernel.Bind<IReggieBasic>().To<ReggieBasicViewModel>().InSingletonScope();
		}

		protected override object GetInstance(Type serviceType, string key)
		{
			return m_kernel.Get(serviceType);
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return m_kernel.GetAll(serviceType);
		}

		protected override void BuildUp(object instance)
		{
			m_kernel.Inject(instance);
		}

		protected override IEnumerable<Assembly> SelectAssemblies()
		{
			return new[] {
				Assembly.GetAssembly(typeof(Reggie.UI.Views.ReggieBasicView)),
				Assembly.GetExecutingAssembly()
			};
		}
	}
}

