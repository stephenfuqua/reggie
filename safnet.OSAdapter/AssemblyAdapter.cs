using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace safnet.SystemAdapters
{
    /// <summary>
    /// Interface of Assembly functions.
    /// </summary>
    public interface IAssemblyAdapter
    {
        string GetVersionNumber();
        string GetApplicationName();
        void ExitApplication();
    }

    /// <summary>
    /// Adapter for Assembly functions (e.g. Exit, Version Number).
    /// </summary>
    public class AssemblyAdapter : IAssemblyAdapter
    {
        /// <summary>
        /// Returns the entry assembly's version number, or the calling assembly.
        /// </summary>
        /// <returns>string</returns>
        public string GetVersionNumber()
        {
            return (Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).GetName().Version.ToString();
        }

        /// <summary>
        /// Returns the entry assembly's name, or the calling assembly.
        /// </summary>
        /// <returns>string</returns>
        public string GetApplicationName()
        {
            return (Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).GetName().Name;
        }

        /// <summary>
        /// Closes / exits the application.
        /// </summary>
        public void ExitApplication()
        {
            Environment.Exit(0);
        }
    }
}
