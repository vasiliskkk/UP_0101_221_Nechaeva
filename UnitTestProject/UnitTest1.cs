using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewProject.Pages;
using System;
using NewProject;

namespace NewProject.Pages
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var page = new AuthPage();
            Assert.IsTrue(page.Auth("nataly", "12345", false));
            Assert.IsFalse(page.Auth("invaliduser", "wrongpassword", false));
        }
    }
}
