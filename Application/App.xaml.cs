using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Reggie
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    MainWindow window = new MainWindow();

        //    var viewModel = new Reggie.UI.ViewModels.MainWindowViewModel();

        //    // When the ViewModel asks to be closed, close the window.
        //    EventHandler handler = null;
        //    handler = delegate
        //    {
        //        viewModel.RequestClose -= handler;
        //        window.Close();
        //    };
        //    viewModel.RequestClose += handler;

        //    viewModel.AboutOpen += delegate
        //    {
        //        Reggie.UI.Views.About about = new UI.Views.About();
        //        about.ShowDialog();
        //    };

        //    // Allow all controls in the window to 
        //    // bind to the ViewModel by setting the 
        //    // DataContext, which propagates down 
        //    // the element tree.
        //    window.DataContext = viewModel;

        //    window.Show();
        //}
    }
}
