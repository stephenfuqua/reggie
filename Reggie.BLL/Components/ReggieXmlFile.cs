using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reggie.BLL.Entities;
using safnet.SystemAdapters;

namespace Reggie.BLL.Components
{
    /// <summary>
    /// Saves or retrieves a <see cref="IReggieSession"/> to/from an XML file.
    /// </summary>
    public class ReggieXmlFile : ISessionPersistence
    {

        private const string ReggieExtension = "*.reggie";
        private const string ReggieFilter = "Reggie file (*.reggie)|*.reggie";

        private IFileAdapter m_fileAdapter;

        /// <summary>
        /// Creates a new instance of <see cref="ReggieXmlFile"/>.
        /// </summary>
        /// <param name="fileAdapter"></param>
        public ReggieXmlFile(IFileAdapter fileAdapter)
        {
            m_fileAdapter = fileAdapter;
        }

        /// <summary>
        /// Saves the <see cref="IReggieSession"/>.
        /// </summary>
        /// <param name="session">Session to save</param>
        public void Save(ReggieSession session)
        {
            string file = m_fileAdapter.OpenFileSaveDialogBox(ReggieExtension, ReggieFilter);

            if (!string.IsNullOrWhiteSpace(file))
            {
                m_fileAdapter.SerializeXmlFile(new[] { session }, file);
            }
        }

        /// <summary>
        /// Retrieves a <see cref="IReggieSession"/>.
        /// </summary>
        /// <returns>Instance of <see cref="IReggieSession"/></returns>
        public ReggieSession Retrieve()
        {
            string file = m_fileAdapter.OpenFileOpenDialogBox(ReggieExtension, ReggieFilter);

            if (string.IsNullOrWhiteSpace(file))
            {
                return null;
            }

            var sessions = m_fileAdapter.DeserializeXmlFile<ReggieSession>(file);

            if (sessions.Count() > 0)
            {
                return sessions[0];
            }

            return null;
        }
    }
}
