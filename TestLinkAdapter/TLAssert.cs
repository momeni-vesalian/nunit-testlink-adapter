using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;
using static Meyn.TestLink.TestCaseResultStatus;


namespace NUnit.TestLink
{
    /// <summary>
    /// The Assert class contains a collection of static methods that
    /// implement the most common assertions used in NUnit. 
    /// You can add your methods as you need
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class TLAssert
    {
        private static readonly TestLinkAdapter Adapter = new TestLinkAdapter();

        private TLAssert()
        {
        }

        /// <summary>
        /// <returns>Method that calls the current method</returns>
        /// </summary>
        /// <returns></returns>
        private static MethodBase GetCallerMethod()
        {
            // ReSharper disable once PossibleNullReferenceException
            return new StackTrace().GetFrames()[2].GetMethod();
        }

        /// <summary>
        /// <returns>true if the method have ExpectedException attribute with AssertionException value</returns>
        /// </summary>
        private static bool ExceptionExpected(MethodBase callerMethod, Exception exception)
        {
            foreach (var customAttribute in callerMethod.GetCustomAttributes(false))
            {
                if ((customAttribute is ExpectedExceptionAttribute) && ((customAttribute as ExpectedExceptionAttribute).ExpectedExceptionName == "NUnit.Framework.AssertionException"))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Default message that is passed to TestLink when test case is passed successfully.
        /// </summary>
        /// <returns></returns>
        private static String PassedMessage()
        {
            return "Test case is passed at " + DateTime.Now;
        }

        /// <summary>
        /// Default message that is passed to TestLink when test case is failed.
        /// </summary>
        /// <returns></returns>
        private static String FailedMessage()
        {
            return "Test case is passed at " + DateTime.Now;
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsTrue(bool condition, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsTrue(condition, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        public static void IsTrue(bool condition)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsTrue(condition);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Asserts that a condition is false. If the condition is true the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsFalse(bool condition, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsFalse(condition, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Asserts that a condition is false. If the condition is true the method throws
        /// an <see cref="T:NUnit.Framework.AssertionException" />.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        public static void IsFalse(bool condition)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsFalse(condition);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Verifies that the object that is passed in is not equal to <code>null</code>
        /// If the object is <code>null</code> then an <see cref="T:NUnit.Framework.AssertionException" />
        /// is thrown.
        /// </summary>
        /// <param name="anObject">The object that is to be tested</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsNotNull(object anObject, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNotNull(anObject, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Verifies that the object that is passed in is not equal to <code>null</code>
        /// If the object is <code>null</code> then an <see cref="T:NUnit.Framework.AssertionException" />
        /// is thrown.
        /// </summary>
        /// <param name="anObject">The object that is to be tested</param>
        public static void IsNotNull(object anObject)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNotNull(anObject);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Verifies that the object that is passed in is equal to <code>null</code>
        /// If the object is not <code>null</code> then an <see cref="T:NUnit.Framework.AssertionException" />
        /// is thrown.
        /// </summary>
        /// <param name="anObject">The object that is to be tested</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsNull(object anObject, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNull(anObject, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Verifies that the object that is passed in is equal to <code>null</code>
        /// If the object is not <code>null</code> then an <see cref="T:NUnit.Framework.AssertionException" />
        /// is thrown.
        /// </summary>
        /// <param name="anObject">The object that is to be tested</param>
        public static void IsNull(object anObject)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNull(anObject);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Verifies that two ints are equal. If they are not, then an
        /// <see cref="T:NUnit.Framework.AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void AreEqual<T>(T expected, T actual, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.AreEqual(expected, actual, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Verifies that two ints are equal. If they are not, then an
        /// <see cref="T:NUnit.Framework.AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        public static void AreEqual<T>(T expected, T actual)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.AreEqual(expected, actual);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Verifies that two ints are not equal. If they are equal, then an
        /// <see cref="T:NUnit.Framework.AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void AreNotEqual<T>(T expected, T actual, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.AreNotEqual(expected, actual, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Verifies that two ints are not equal. If they are equal, then an
        /// <see cref="T:NUnit.Framework.AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        public static void AreNotEqual<T>(T expected, T actual)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.AreNotEqual(expected, actual);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Asserts that two objects refer to the same object. If they
        /// are not the same an <see cref="T:NUnit.Framework.AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void AreSame<T>(T expected, T actual, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.AreSame(expected, actual, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Asserts that two objects refer to the same object. If they
        /// are not the same an <see cref="T:NUnit.Framework.AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        public static void AreSame<T>(T expected, T actual)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.AreSame(expected, actual);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Asserts that two objects do not refer to the same object. If they
        /// are the same an <see cref="T:NUnit.Framework.AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void AreNotSame<T>(T expected, T actual, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.AreNotSame(expected, actual, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Asserts that two objects do not refer to the same object. If they
        /// are the same an <see cref="T:NUnit.Framework.AssertionException" /> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        public static void AreNotSame<T>(T expected, T actual)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.AreNotSame(expected, actual);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Assert that a string is empty - that is equal to string.Empty
        /// </summary>
        /// <param name="aString">The string to be tested</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsEmpty(string aString, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsEmpty(aString, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Assert that a string is empty - that is equal to string.Empty
        /// </summary>
        /// <param name="aString">The string to be tested</param>
        public static void IsEmpty(string aString)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsEmpty(aString);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty
        /// </summary>
        /// <param name="collection">An array, list or other collection implementing ICollection</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsEmpty(IEnumerable collection, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsEmpty(collection, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Assert that an array, list or other collection is empty
        /// </summary>
        /// <param name="collection">An array, list or other collection implementing ICollection</param>
        public static void IsEmpty(IEnumerable collection)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsEmpty(collection);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Assert that a string is not empty - that is not equal to string.Empty
        /// </summary>
        /// <param name="aString">The string to be tested</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsNotEmpty(string aString, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNotEmpty(aString, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Assert that a string is not empty - that is not equal to string.Empty
        /// </summary>
        /// <param name="aString">The string to be tested</param>
        public static void IsNotEmpty(string aString)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNotEmpty(aString);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty
        /// </summary>
        /// <param name="collection">An array, list or other collection implementing ICollection</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsNotEmpty(IEnumerable collection, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNotEmpty(collection, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Assert that an array, list or other collection is not empty
        /// </summary>
        /// <param name="collection">An array, list or other collection implementing ICollection</param>
        public static void IsNotEmpty(IEnumerable collection)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNotEmpty(collection);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>
        /// Assert that a string is either null or equal to string.Empty
        /// </summary>
        /// <param name="aString">The string to be tested</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsNullOrEmpty(string aString, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNullOrEmpty(aString, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>
        /// Assert that a string is either null or equal to string.Empty
        /// </summary>
        /// <param name="aString">The string to be tested</param>
        public static void IsNullOrEmpty(string aString)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.IsNullOrEmpty(aString);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }

        /// <summary>Asserts that an object is contained in a list.</summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The list to be examined</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void Contains(object expected, ICollection actual, string message)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.Contains(expected, actual, message);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, message);
                throw e;
            }
        }

        /// <summary>Asserts that an object is contained in a list.</summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The list to be examined</param>
        public static void Contains(object expected, ICollection actual)
        {
            MethodBase callerMethod = GetCallerMethod();
            try
            {
                Assert.Contains(expected, actual);
                Adapter.SendToTestlink(callerMethod, Pass, PassedMessage());
            }
            catch (Exception e)
            {
                Adapter.SendToTestlink(callerMethod, ExceptionExpected(callerMethod, e) ? Pass : Fail, FailedMessage());
                throw e;
            }
        }
    }
}