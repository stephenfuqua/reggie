using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reggie.BLL.Entities;

namespace Reggie.BLL.Components
{
    /// <summary>
    /// Abstract "contract" for saving and retrieving <see cref="ReggieSession"/>s to and from any data source.
    /// </summary>
    public interface ISessionPersistence
    {
        /// <summary>
        /// Saves the <see cref="ReggieSession"/>.
        /// </summary>
        /// <param name="session">Session to save</param>
        void Save(ReggieSession session);

        /// <summary>
        /// Retrieves a <see cref="ReggieSession"/>.
        /// </summary>
        /// <returns>Instance of <see cref="IReggieSession"/></returns>
        ReggieSession Retrieve();
    }
}
