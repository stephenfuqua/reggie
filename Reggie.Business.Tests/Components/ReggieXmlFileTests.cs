using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reggie.BLL.Components;
using safnet.SystemAdapters;
using Moq;
using Reggie.BLL.Entities;

namespace Reggie.BLL.Tests.Components
{
    [TestClass]
    public class ReggieXmlFileTests
    {
        private Mock<IFileAdapter> m_fileAdapter;
        private ReggieXmlFile m_systemUnderTest;
        private MockRepository m_mockFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            m_mockFactory = new MockRepository(MockBehavior.Strict);
            m_fileAdapter = m_mockFactory.Create<IFileAdapter>();
            m_systemUnderTest = new ReggieXmlFile(m_fileAdapter.Object);
        }

        [TestMethod]
        public void SaveSessionToXmlFile()
        {
            // Prepare input
            ReggieSession session = new ReggieSession() { RegularExpressionPattern = "323", SampleText = "46346kljlk" };
            string fileToOpen = "c:\\this\\file.reggie";

            // Prepare mocks
            m_fileAdapter.Setup(fa => fa.OpenFileSaveDialogBox(It.Is<string>(x => x == "*.reggie"),
                                                             It.Is<string>(x => x == ("Reggie file (*.reggie)|*.reggie"))))
                         .Returns(fileToOpen);
            m_fileAdapter.Setup(fa => fa.SerializeXmlFile<ReggieSession>(It.Is<ReggieSession[]>(x => object.ReferenceEquals(x[0], session)),
                                                                          It.Is<string>(x => x== fileToOpen)));

            // Call the system under test
            m_systemUnderTest.Save(session);

            // Evaluate output
            m_fileAdapter.Verify(fa => fa.OpenFileSaveDialogBox(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            m_fileAdapter.Verify(fa => fa.SerializeXmlFile<ReggieSession>(It.IsAny<ReggieSession[]>(), 
                                                                           It.IsAny<string>()), Times.Once());
        }


        [TestMethod]
        public void SaveSessionToXmlFileButDialogIsCancelled()
        {
            // Prepare input
            ReggieSession session = new ReggieSession() { RegularExpressionPattern = "323", SampleText = "46346kljlk" };
            string fileToOpen = string.Empty;

            // Prepare mocks
            m_fileAdapter.Setup(fa => fa.OpenFileSaveDialogBox(It.Is<string>(x => x == "*.reggie"),
                                                             It.Is<string>(x => x == ("Reggie file (*.reggie)|*.reggie"))))
                         .Returns(fileToOpen);

            // Call the system under test
            m_systemUnderTest.Save(session);

            // Evaluate output
            m_fileAdapter.Verify(fa => fa.OpenFileSaveDialogBox(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void RetrieveSessionFromXmlFile()
        {
            // Prepare input
            ReggieSession session = new ReggieSession() { RegularExpressionPattern = "323", SampleText = "46346kljlk" };
            string fileToOpen = "c:\\this\\file.reggie";

            // Prepare mocks
            m_fileAdapter.Setup(fa => fa.OpenFileOpenDialogBox(It.Is<string>(x => x == "*.reggie"),
                                                             It.Is<string>(x => x == ("Reggie file (*.reggie)|*.reggie"))))
                         .Returns(fileToOpen);
            m_fileAdapter.Setup(fa => fa.DeserializeXmlFile<ReggieSession>(It.Is<string>(x => x == fileToOpen)))
                         .Returns(new[]{session});

            // Call the system under test
            var actual = m_systemUnderTest.Retrieve();

            // Evaluate output
            Assert.IsTrue(object.ReferenceEquals(session, actual), "wrong value returned");

            m_fileAdapter.Verify(fa => fa.OpenFileOpenDialogBox(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            m_fileAdapter.Verify(fa => fa.DeserializeXmlFile<ReggieSession>(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public void RetrieveSessionFromAnEmptyFile()
        {
            // Prepare input
            ReggieSession session = null;
            string fileToOpen = "c:\\this\\file.reggie";

            // Prepare mocks
            m_fileAdapter.Setup(fa => fa.OpenFileOpenDialogBox(It.Is<string>(x => x == "*.reggie"),
                                                             It.Is<string>(x => x == ("Reggie file (*.reggie)|*.reggie"))))
                         .Returns(fileToOpen);
            m_fileAdapter.Setup(fa => fa.DeserializeXmlFile<ReggieSession>(It.Is<string>(x => x == fileToOpen)))
                         .Returns(new ReggieSession[0]);

            // Call the system under test
            var actual = m_systemUnderTest.Retrieve();

            // Evaluate output
            Assert.IsTrue(object.ReferenceEquals(session, actual), "wrong value returned");

            m_fileAdapter.Verify(fa => fa.OpenFileOpenDialogBox(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            m_fileAdapter.Verify(fa => fa.DeserializeXmlFile<ReggieSession>(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void RetrieveSessionFromBadFile()
        {
            // Prepare input
            ReggieSession session = new ReggieSession() { RegularExpressionPattern = "323", SampleText = "46346kljlk" };
            string fileToOpen = "c:\\this\\file.reggie";

            // Prepare mocks
            m_fileAdapter.Setup(fa => fa.OpenFileOpenDialogBox(It.Is<string>(x => x == "*.reggie"),
                                                             It.Is<string>(x => x == ("Reggie file (*.reggie)|*.reggie"))))
                         .Returns(fileToOpen);
            m_fileAdapter.Setup(fa => fa.DeserializeXmlFile<ReggieSession>(It.Is<string>(x => x == fileToOpen)))
                         .Throws<System.InvalidOperationException>();

            // Call the system under test
            var actual = m_systemUnderTest.Retrieve();

        }



        [TestMethod]
        public void RetrieveSessionFromXmlFileButDialogIsCancelled()
        {
            // Prepare input
            ReggieSession session = null;
            string fileToOpen = string.Empty;

            // Prepare mocks
            m_fileAdapter.Setup(fa => fa.OpenFileOpenDialogBox(It.Is<string>(x => x == "*.reggie"),
                                                             It.Is<string>(x => x == ("Reggie file (*.reggie)|*.reggie"))))
                         .Returns(fileToOpen);

            // Call the system under test
            var actual = m_systemUnderTest.Retrieve();

            // Evaluate output
            Assert.IsTrue(object.ReferenceEquals(session, actual), "wrong value returned");

            m_fileAdapter.Verify(fa => fa.OpenFileOpenDialogBox(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}