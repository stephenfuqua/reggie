using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Reggie.BLL.Entities;
using System.Windows.Input;

namespace Reggie.UI.ViewModels
{
    /// <summary>
    /// ViewModel for the ReggieBasicView.
    /// </summary>
    public sealed class RegexViewModel : ViewModelBase
    {
        private RegExTest m_regEx;
        private string m_result;

        /// <summary>
        /// Gets or sets a <see cref="RegExTest"/> instance.
        /// </summary>
        /// <value>The <see cref="RegExTest"/> instance.</value>
        public RegExTest RegExTest
        {
            get
            {
                if (m_regEx == null)
                {
                    m_regEx = new RegExTest();
                }
                return m_regEx;
            }
            set
            {
                m_regEx = value;
                base.OnPropertyChanged("RegExTest");
            }
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public string Result
        {
            get
            {
                return m_result;
            }
            private set
            {
                m_result = value;
                base.OnPropertyChanged("Result");
            }
        }

        ICommand m_executeCommand;

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        public ICommand Execute
        {
            get
            {
                if (m_executeCommand == null)
                {
                    m_executeCommand = new RelayCommand(param =>
                        {
                            Result = m_regEx.TryPatternMatch();
                        });
                }
                return m_executeCommand;
            }
        }
    }
}
