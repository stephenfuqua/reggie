using System.Diagnostics;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reggie.UI.ViewModels;
using Moq;
using Caliburn.Micro;
using Reggie.UI.Utility;
using Reggie.BLL.Components;
using Reggie.BLL.Entities;
using safnet.SystemAdapters;

namespace Reggie.UI.Tests
{
    /// <summary>
    /// Unit tests for <see cref="ReggieBasicViewModel"/>.
    /// </summary>
    [TestClass]
    public class ReggieBasicViewModelTests
    {
        private MockRepository m_mockFactory = new MockRepository(MockBehavior.Strict);
        private Mock<IHelperFactory> m_helperFactory;
        private Mock<IFileAdapter> m_fileAdapter;
        private Mock<ISessionPersistence> m_persistence;
        private Mock<IAssemblyAdapter> m_assemblyAdapter;
        private Mock<IWindowManager> m_windowManager;
        private Mock<IAbout> m_about;

        private ReggieBasicViewModel m_systemUnderTest;

        [TestInitialize]
        public void TestInitialize()
        {
            // Create mocks
            m_helperFactory = m_mockFactory.Create<IHelperFactory>();
            m_windowManager = m_mockFactory.Create<IWindowManager>();
            m_helperFactory = m_mockFactory.Create<IHelperFactory>();
            m_assemblyAdapter = m_mockFactory.Create<IAssemblyAdapter>();
            m_persistence = m_mockFactory.Create<ISessionPersistence>();
            m_fileAdapter = m_mockFactory.Create<IFileAdapter>();
            m_about = m_mockFactory.Create<IAbout>();

            // Setup default method invocations
            m_helperFactory.Setup(hf => hf.BuildAssemblyAdapter())
                           .Returns(m_assemblyAdapter.Object);
            m_helperFactory.Setup(hf => hf.BuildFileAdapter())
                           .Returns(m_fileAdapter.Object);
            m_helperFactory.Setup(hf => hf.BuildAboutScreen(It.Is<IWindowManager>(x => object.ReferenceEquals(x, m_windowManager.Object))))
                           .Returns(m_about.Object);
            m_helperFactory.Setup(hf => hf.BuildPersistenceService())
                           .Returns(m_persistence.Object);

            // Setup the system under test
            m_systemUnderTest = new ReggieBasicViewModel(m_windowManager.Object, m_helperFactory.Object);
        }

        [TestMethod]
        public void GetAndSetDisplayName()
        {
            // Prepare Input
            string expected = "asdfasdfa asdf a";

            // Call the system under test
            m_systemUnderTest.DisplayName = expected;

            // Evaluate results         
            string actual = m_systemUnderTest.DisplayName;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAndSetSampleText()
        {
            // Prepare Input
            string expected = "asdfasdfa asdf a";

            // Call the system under test
            m_systemUnderTest.SampleText = expected;

            // Evaluate results         
            string actual = m_systemUnderTest.SampleText;
            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void SetSampleTextTriggersNotification()
        {
            // Prepare Input
            bool notificationFired = false;
            m_systemUnderTest.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                notificationFired = true;
                Assert.AreEqual("SampleText", e.PropertyName, "wrong notification");
            };

            string expected = "asdfasdfa asdf a";

            // Call the system under test
            m_systemUnderTest.SampleText = expected;

            // Evaluate results         
            Assert.IsTrue(notificationFired);
        }


        [TestMethod]
        public void GetAndRegularExpressionPattern()
        {
            // Prepare Input
            string expected = "asdfasdfa asdf a";

            // Call the system under test
            m_systemUnderTest.RegularExpressionPattern = expected;

            // Evaluate results         
            string actual = m_systemUnderTest.RegularExpressionPattern;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetRegularExpressionPatternTriggersNotification()
        {
            // Prepare Input
            bool regExNotificationFired = false;
            bool canExecuteNotificationFired = false;
            m_systemUnderTest.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                regExNotificationFired = regExNotificationFired || e.PropertyName == "RegularExpressionPattern";
                canExecuteNotificationFired = canExecuteNotificationFired || e.PropertyName == "CanExecute";
            };

            string expected = "asdfasdfa asdf a";

            // Call the system under test
            m_systemUnderTest.RegularExpressionPattern = expected;

            // Evaluate results         
            Assert.IsTrue(regExNotificationFired, "Regex notification not fired");
            Assert.IsTrue(canExecuteNotificationFired, "CanExecute notification not fired");
        }

        [TestMethod]
        public void OpenAboutWindowCreatesAboutWindow()
        {
            // Prepare Input

            // Build up the Mocks as needed
            m_windowManager.Setup(wm => wm.ShowWindow(It.IsAny<IAbout>(),
                                                    It.Is<object>(x => x == null),
                                                    It.Is<IDictionary<string, object>>(x => x == null)));

            // Call the system under test
            m_systemUnderTest.OpenAboutWindow();

            // Evaluate results
            m_windowManager.Verify(wm => wm.ShowWindow(It.IsAny<IAbout>(),
                                                    It.Is<object>(x => x == null),
                                                    It.Is<IDictionary<string, object>>(x => x == null)), Times.Once());
        }

        [TestMethod]
        public void CanExecuteIsFalseWhenPatternIsNull()
        {
            // Prepare Input
            m_systemUnderTest.RegularExpressionPattern = null;

            // Call the system under test
            bool actual = m_systemUnderTest.CanExecute;
            bool exptected = false;

            // Evaluate results
            Assert.AreEqual(exptected, actual);
        }

        [TestMethod]
        public void CanExecuteIsFalseWhenPatternIsAnEmptyString()
        {
            // Prepare Input
            m_systemUnderTest.RegularExpressionPattern = string.Empty;

            // Call the system under test
            bool actual = m_systemUnderTest.CanExecute;
            bool exptected = false;

            // Evaluate results
            Assert.AreEqual(exptected, actual);
        }

        [TestMethod]
        public void CanExecuteIsFalseWhenPatternIsABunchOfSpaces()
        {
            // Prepare Input
            m_systemUnderTest.RegularExpressionPattern = "        ";

            // Call the system under test
            bool actual = m_systemUnderTest.CanExecute;
            bool exptected = false;

            // Evaluate results
            Assert.AreEqual(exptected, actual);
        }

        [TestMethod]
        public void CanExecuteIsTrueWhenPatternARealString()
        {
            // Prepare Input
            m_systemUnderTest.RegularExpressionPattern = "s        ";

            // Call the system under test
            bool actual = m_systemUnderTest.CanExecute;
            bool exptected = true;

            // Evaluate results
            Assert.AreEqual(exptected, actual);
        }

        [TestMethod]
        public void ExitApplicationClosesTheApplication()
        {
            // Prepare Input

            // Setup mocks
            m_assemblyAdapter.Setup(e => e.ExitApplication());

            // Call the system under test
            m_systemUnderTest.ExitApplication();

            // Evaluate results
            m_assemblyAdapter.Verify(e => e.ExitApplication(), Times.Once());
        }

        [TestMethod]
        public void OpenSessionLoadsSessionfromPersistenceService()
        {
            // Prepare Input
            string sampleText = "Reggie";
            string regularExpressionPattern = "^(Reggie)$";
            ReggieSession session = new ReggieSession()
            {
                RegularExpressionPattern = regularExpressionPattern,
                SampleText = sampleText
            };

            // Setup mocks
            m_persistence.Setup(p => p.Retrieve())
                         .Returns(session);

            // Call the system udner test
            m_systemUnderTest.OpenSession();

            // Evalute results
            Assert.AreEqual(sampleText, m_systemUnderTest.SampleText, "SampleText");
            Assert.AreEqual(regularExpressionPattern, m_systemUnderTest.RegularExpressionPattern, "RegularExpressionPattern");

            // mock Verfiication unnecessary
        }

        [TestMethod]
        public void SaveSessionLoadsSessionIntoPersistenceService()
        {
            // Prepare Input
            string sampleText = "Reggie";
            string regularExpressionPattern = "^(Reggie)$";

            // Setup mocks
            m_persistence.Setup(p => p.Save(It.Is<ReggieSession>(x => x.SampleText == sampleText 
                                                                    && x.RegularExpressionPattern == regularExpressionPattern)));

            // Call the system under test
            m_systemUnderTest.SampleText = sampleText;
            m_systemUnderTest.RegularExpressionPattern = regularExpressionPattern;
            m_systemUnderTest.SaveSession();

            // Evaluate results
            m_persistence.Verify(p => p.Save(It.IsAny<ReggieSession>()), Times.Once());
        }
    }
}
