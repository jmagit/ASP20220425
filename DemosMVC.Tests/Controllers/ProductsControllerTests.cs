using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemosMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Services;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemosMVC.Controllers.Tests {
    [TestClass()]
    public class ProductsControllerTests {
        [TestMethod()]
        public void IndexTest() {
            var controlador = new ProductsController(null, new ProductoService(new ProductoRepositoryMock()));
            var rslt = controlador.Index(0, 10);
            Assert.IsNotNull(rslt);
            Assert.IsInstanceOfType(rslt, typeof(ViewResult));
            //Assert.AreEqual(3, (rslt.Model as List<ProductoDTO>).Count);
        }
    }
}