using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reggie.UI.Common
{
    /// <summary>
    /// Interface defining a modal dialog.
    /// </summary>
    /// <remarks>
    /// Courtesy of http://blog.roboblob.com/2010/01/19/modal-dialogs-with-mvvm-and-silverlight-4/
    /// </remarks>
    public interface IModalWindow
    {
        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        /// <value>The dialog result.</value>
        bool? DialogResult { get; set; }

        /// <summary>
        /// Occurs when the dialog is closed.
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        void Show();

        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        /// <value>The data context.</value>
        object DataContext { get; set; }
        
        /// <summary>
        /// Closes the dialog.
        /// </summary>
        void Close();
    }

}
