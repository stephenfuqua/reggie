using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reggie.BLL.Entities;

namespace Reggie.BLL.Tests.Entities
{
    [TestClass]
    public class RessieSessionTests
    {
        /// <summary>
        /// Evaluates the SampleText property.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void GetAndSetSampleText()
        {
            string expected = "asdf34;lkjsdf";

            ReggieSession target = new ReggieSession();

            target.SampleText = expected;

            string actual = target.SampleText;

            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        /// Evaluates the RegularExpressionPattern property.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void GetAndSetRegularExpressionPattern()
        {
            string expected = "asdf34;lkjsdf";

            ReggieSession target = new ReggieSession();

            target.RegularExpressionPattern = expected;

            string actual = target.RegularExpressionPattern;

            Assert.AreEqual(expected, actual);
        }




        //        /// <summary>
        //        ///A test for RegExTest Constructor
        //        ///</summary>
        //        [TestMethod()]
        //        public void t_RegExTestConstructor()
        //        {
        //            RegExTest target = new RegExTest();
        //            Assert.IsNotNull(target);
        //        }

        //        /// <summary>
        //        ///A test for TryPatternMatch with a null Pattern
        //        ///</summary>
        //        [TestMethod()]
        //        [ExpectedException(typeof(InvalidOperationException))]
        //        public void t_TryPatternMatch_NullPattern()
        //        {
        //            string pattern = null;
        //            string teststring = "string.empty";
        //            RegExTest target = new RegExTest()
        //            {
        //                Pattern = pattern,
        //                TestString = teststring
        //            };
        //            target.TryPatternMatch();
        //        }

        //        /// <summary>
        //        ///A test for TryPatternMatch with a null TestString
        //        ///</summary>
        //        [TestMethod()]
        //        [ExpectedException(typeof(InvalidOperationException))]
        //        public void t_TryPatternMatch_TestString()
        //        {
        //            string pattern = "string.empty";
        //            string teststring = null;
        //            RegExTest target = new RegExTest()
        //            {
        //                Pattern = pattern,
        //                TestString = teststring
        //            };
        //            target.TryPatternMatch();
        //        }

        //        /// <summary>
        //        /// A test for TryPatternMatch with input "this is my input" and pattern "\w+" (complete word)
        //        /// </summary>
        //        [TestMethod]
        //        public void t_TryPatternMatch_WordMatch()
        //        {
        //            string testString = "this is my input";
        //            string pattern = "(\\w+)";
        //            string expected = "[1] this" + Environment.NewLine + "[2] is" + Environment.NewLine + "[3] my" + Environment.NewLine + "[4] input" + Environment.NewLine;

        //            RegExTest target = new RegExTest()
        //            {
        //                Pattern = pattern,
        //                TestString = testString
        //            };

        //            string actual = target.TryPatternMatch();
        //            Assert.AreEqual(expected, actual);
        //        }

        //        /// <summary>
        //        /// A test for TryPatternmatch with a basic phone number pattern and a valid phone number
        //        /// </summary>
        //        [TestMethod]
        //        public void t_TryPattern_PhoneNumber()
        //        {
        //            string testString = "(651) 555-5554";
        //            string pattern = @"^[01]?[- .]?\(?[2-9]\d{2}\)?[- .]?\d{3}[- .]?\d{4}$";
        //            string expected = "[1] (651) 555-5554" + Environment.NewLine;

        //            RegExTest target = new RegExTest()
        //            {
        //                Pattern = pattern,
        //                TestString = testString
        //            };

        //            string actual = target.TryPatternMatch();
        //            Assert.AreEqual(expected, actual);
        //        }

        //        /// <summary>
        //        /// A test for TryPatternmatch with a basic phone number pattern and an INVALID phone number
        //        /// </summary>
        //        [TestMethod]
        //        public void t_TryPattern_PhoneNumber_NoMatch()
        //        {
        //            string testString = "(651) 555-554"; // missing final digit
        //            string pattern = @"^[01]?[- .]?\(?[2-9]\d{2}\)?[- .]?\d{3}[- .]?\d{4}$";
        //            string expected = "No matches found.";

        //            RegExTest target = new RegExTest()
        //            {
        //                Pattern = pattern,
        //                TestString = testString
        //            };

        //            string actual = target.TryPatternMatch();
        //            Assert.AreEqual(expected, actual);
        //        }

        //        /// <summary>
        //        ///A test for Pattern
        //        ///</summary>
        //        [TestMethod()]
        //        public void t_Pattern()
        //        {
        //            RegExTest target = new RegExTest();
        //            string expected = "string.Empty";
        //            string actual;
        //            target.Pattern = expected;
        //            actual = target.Pattern;
        //            Assert.AreEqual(expected, actual);
        //        }

        //        /// <summary>
        //        ///A test for TestString
        //        ///</summary>
        //        [TestMethod()]
        //        public void t_TestString()
        //        {
        //            RegExTest target = new RegExTest();
        //            string expected = "string.Empty";
        //            string actual;
        //            target.TestString = expected;
        //            actual = target.TestString;
        //            Assert.AreEqual(expected, actual);
        //        }
    }
}