using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Caliburn.Micro;
using Reggie.UI.Utility;
using safnet.SystemAdapters;

namespace Reggie.UI.ViewModels
{
    public interface IAbout
    {
        string Version { get; }
        void CloseWindow();
        void OpenReggieWebsite();
        void OpenSafnet();
    }

    public class AboutViewModel : Screen, IAbout
    {
        private readonly IWindowManager m_windowManager;
        private readonly IAssemblyAdapter m_assemblyAdapter;
        private readonly IFileAdapter m_fileAdapter;

        private const string c_reggieWebsite = "http://reggie.codeplex.com/";
        private const string c_msplWebsite = "http://www.microsoft.com/en-us/openness/licenses.aspx#MPL";
        private const string c_safnetWebsite = "http://www.safnet.com";

        /// <summary>
        /// Creates a new instance of <see cref="AboutViewModel"/>.
        /// </summary>
        /// <param name="windowManager"></param>
        /// <param name="fileAdapter"></param>
        /// <param name="assemblyAdapter"></param>
        public AboutViewModel(IWindowManager windowManager, IHelperFactory helperFactory)
        {
            if (windowManager == null)
            {
                throw new ArgumentNullException("windowManager");
            }
            if (helperFactory == null)
            {
                throw new ArgumentNullException("helperFactory");
            }

            m_windowManager = windowManager;
            m_assemblyAdapter = helperFactory.BuildAssemblyAdapter();
            m_fileAdapter = helperFactory.BuildFileAdapter();
        }

        /// <summary>
        /// Gets the application version number
        /// </summary>
        public string Version
        {
            get
            {
                return m_assemblyAdapter.GetVersionNumber();
            }
        }

        /// <summary>
        /// Sends the signal to close the "Screen" object.
        /// </summary>
        public void CloseWindow()
        {
            base.TryClose();
        }

        /// <summary>
        /// Open the Reggie website.
        /// </summary>
        public void OpenReggieWebsite()
        {
            m_fileAdapter.OpenInDefaultApplication(c_reggieWebsite);
        }

        /// <summary>
        /// Open the End User License Agreement.
        /// </summary>
        public void OpenLicense()
        {
            m_fileAdapter.OpenInDefaultApplication(c_msplWebsite);
        }

        /// <summary>
        /// Open safnet.com.
        /// </summary>
        public void OpenSafnet()
        {
            m_fileAdapter.OpenInDefaultApplication(c_safnetWebsite);
        }

    }

}
