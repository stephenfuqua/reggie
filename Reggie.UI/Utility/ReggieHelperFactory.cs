using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using safnet.SystemAdapters;
using Reggie.BLL.Components;
using Reggie.BLL.Entities;

namespace Reggie.UI.Utility
{
    public interface IHelperFactory
    {
        IFileAdapter BuildFileAdapter();
        IAssemblyAdapter BuildAssemblyAdapter();
        ISessionPersistence BuildPersistenceService();
        Reggie.UI.ViewModels.IAbout BuildAboutScreen(Caliburn.Micro.IWindowManager windowManager);
    }

    /// <summary>
    /// Builds concrete instances of various interfaces used by Reggie.
    /// </summary>
    public class ReggieHelperFactory : IHelperFactory
    {
        public IFileAdapter BuildFileAdapter()
        {
            return new WindowsFileAdapter();
        }

        public IAssemblyAdapter BuildAssemblyAdapter()
        {
            return new AssemblyAdapter();
        }

        public ISessionPersistence BuildPersistenceService()
        {
            return new ReggieXmlFile(BuildFileAdapter());
        }

        public Reggie.UI.ViewModels.IAbout BuildAboutScreen(Caliburn.Micro.IWindowManager windowManager)
        {
            return new Reggie.UI.ViewModels.AboutViewModel(windowManager, this);
        }
    }
}
