﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Clockwork.Web;
using Clockwork.Web.Controllers;
using Clockwork.Web.Models;

namespace Clockwork.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = (ViewResult)controller.Index();

            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            var expectedVersion = mvcName.Version.Major + "." + mvcName.Version.Minor;
            var expectedRuntime = isMono ? "Mono" : ".NET";

            // Assert
            var viewModel = result.Model as HomeViewModel;
            Assert.NotNull(viewModel);
            Assert.AreEqual(expectedVersion, viewModel.Version);
            Assert.AreEqual(expectedRuntime, viewModel.Runtime);
        }
    }
}
