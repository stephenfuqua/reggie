using Caliburn.Micro;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Text;
using System.Dynamic;
using Reggie.UI.Utility;
using Reggie.BLL.Components;
using safnet.SystemAdapters;
using System;
using Reggie.BLL.Entities;

namespace Reggie.UI.ViewModels
{
    public interface IReggieBasic
    {
        string SampleText { get; set; }
        string RegularExpressionPattern { get; set; }
        string Result { get; }
        bool CanExecute { get; }
        void Execute();
        void OpenAboutWindow();

        void SaveSession();
        void OpenSession();
        void ExitApplication();
    }
    

    /// <summary>
    /// MVVM - View Model class for evaluating a regular expression.
    /// </summary>
    public class ReggieBasicViewModel : Screen, IReggieBasic
    {
        private readonly IWindowManager m_windowManager;
        private readonly IFileAdapter m_fileAdapter;
        private readonly ISessionPersistence m_persistence;
        private readonly IAssemblyAdapter m_assemblyAdapter;
        private readonly IHelperFactory m_helperFactory;

        private string m_sampleText;
        private string m_pattern;
        private string m_result;
        private string m_displayName = "Reggie - Regular Expression Tester";

        /// <summary>
        /// Creates a new instance of <see cref="ReggieBasicViewModel"/>.
        /// </summary>
        /// <param name="windowManager"></param>
        public ReggieBasicViewModel(IWindowManager windowManager, IHelperFactory helperFactory)
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
            m_helperFactory = helperFactory;
            m_fileAdapter = helperFactory.BuildFileAdapter();
            m_assemblyAdapter = helperFactory.BuildAssemblyAdapter();
            m_persistence = helperFactory.BuildPersistenceService();

            base.DisplayName = m_displayName;
        }


        #region IReggieBasic Implementation

        /// <summary>
        /// Launch the About window.
        /// </summary>
        public void OpenAboutWindow()
        {
            m_windowManager.ShowWindow(m_helperFactory.BuildAboutScreen(m_windowManager));
        }

        /// <summary>
        /// Gets or sets the sample text to use in the regular expression test.
        /// </summary>
        public string SampleText
        {
            get
            {
                return m_sampleText;
            }
            set
            {
                m_sampleText = value;
                NotifyOfPropertyChange(() => SampleText);
            }
        }

        /// <summary>
        /// Gets or sets the regular expression pattern to test.
        /// </summary>
        public string RegularExpressionPattern
        {
            get
            {
                return m_pattern;
            }
            set
            {
                m_pattern = value;
                NotifyOfPropertyChange(() => RegularExpressionPattern);
                NotifyOfPropertyChange(() => CanExecute);
            }
        }


        /// <summary>
        /// Gets or sets the result of the regular expression testing.
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
                NotifyOfPropertyChange(() => Result);
            }
        }

        /// <summary>
        /// Determines if sufficient information has been loaded to run the regular expression.
        /// </summary>
        public bool CanExecute
        {
            get { return !string.IsNullOrWhiteSpace(this.RegularExpressionPattern); }
        }

        /// <summary>
        /// Runs the regular expression test
        /// </summary>
        public void Execute()
        {
            if (CanExecute)
            {
                Result = TryPatternMatch();
            }
        }

        /// <summary>
        /// Save the current input values for future use.
        /// </summary>
        public void SaveSession()
        {
            var session = new ReggieSession()
            {
                SampleText = this.SampleText,
                RegularExpressionPattern = this.RegularExpressionPattern
            };

            m_persistence.Save(session);
        }

        /// <summary>
        /// Open a previously-saved session.
        /// </summary>
        public void OpenSession()
        {
            var session = m_persistence.Retrieve();

            if (session != null)
            {
                this.SampleText = session.SampleText;
                this.RegularExpressionPattern = session.RegularExpressionPattern;
            }
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        public void ExitApplication()
        {
            m_assemblyAdapter.ExitApplication();
        }
        #endregion

        /// <summary>
        /// Runs the test string through the regular expression pattern.
        /// </summary>
        /// <returns>string describing the match results</returns>
        /// <remarks>
        /// If this method starts getting much bigger, refactor it into another class. As it is, that would be overkill.
        /// </remarks>
        public string TryPatternMatch()
        {
            Regex expression = new Regex(RegularExpressionPattern);
            MatchCollection matches = expression.Matches(SampleText);

            if (matches.Count > 0)
            {
                StringBuilder output = new StringBuilder();
                int lineNumber = 1;
                foreach (Match m in matches)
                {
                    output.Append("[");
                    output.Append(lineNumber++);
                    output.Append("] ");
                    output.AppendLine(m.Value);
                }

                return output.ToString();
            }
            else
            {
                return "No matches found.";
            }
        }
    }
}
