using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reggie.UI.Common
{
    /// <summary>
    /// Interface for classes that help display dialogs.
    /// </summary>
    /// <remarks>
    /// Courtesy of http://blog.roboblob.com/2010/01/19/modal-dialogs-with-mvvm-and-silverlight-4/
    /// </remarks>
    public interface IModalDialogService
    {
        void ShowDialog<TViewModel>(IModalWindow view, TViewModel viewModel, Action<TViewModel> onDialogClose);

        void ShowDialog<TDialogViewModel>(IModalWindow view, TDialogViewModel viewModel);
    }

}
