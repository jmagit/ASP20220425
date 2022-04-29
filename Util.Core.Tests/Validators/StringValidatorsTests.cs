using Microsoft.VisualStudio.TestTools.UnitTesting;
using Util.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities.Core;
using Util.Core.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Util.Core.Validators.Tests {
    [TestClass()]
    public class StringValidatorsTests {
        [TestMethod()]
        [DataRow("12345678z")]
        [DataRow("12345678Z")]
        [DataRow("1234S")]
        public void IsNIFOKTest(string nif) {
            Assert.IsTrue(nif.IsNIF());
        }

        [TestMethod()]
        [DataRow("12345678")]
        [DataRow("Z12345678")]
        [DataRow("4g")]
        [DataRow(null)]
        public void IsNIFKOTest(string nif) {
            Assert.IsFalse(nif.IsNIF());
        }

        [TestMethod()]
        public void IsNotNIFTest() {

        }

        [TestMethod()]
        public void IsMaxLengthTest() {

        }

        [TestMethod()]
        public void IsNotMaxLengthTest() {

        }
    }

    [TestClass()]
    public class NIFAttributeTests {
        class NifMock : NIFAttribute {
            public ValidationResult IsValid(string cad) { return base.IsValid(cad, null); }
        }
        NifMock attr = new NifMock();

        [TestMethod()]
        [DataRow("12345678z")]
        [DataRow("12345678Z")]
        [DataRow("1234S")]
        [DataRow(null)]
        public void NIFValidTest(string nif) {
            Assert.AreEqual(ValidationResult.Success, attr.IsValid(nif));
        }

        [TestMethod()]
        [DataRow("12345678")]
        [DataRow("Z")]
        [DataRow("1234J")]
        [DataRow("")]
        public void NIFInvalidTest(string nif) {
            var rslt = attr.IsValid(nif);
            Assert.AreNotEqual(ValidationResult.Success, rslt);
            Assert.AreEqual("No es un NIF válido.", rslt.ErrorMessage);
        }
    }
    [TestClass()]
    public class NIFComoAtributoTests {
        class EntityMock : EntityBase {
            [NIF]
            public string NIF { get; set; }
        }
        EntityMock entidad = new EntityMock();

        [TestMethod()]
        [DataRow("12345678z")]
        [DataRow("12345678z")]
        public void NIFValidTest(string nif) {
            entidad.NIF = nif;
            Assert.IsTrue(entidad.IsValid);
        }

        [TestMethod()]
        [DataRow("12345678")]
        [DataRow("12345678")]
        public void NIFInvalidTest(string nif) {
            entidad.NIF = nif;
            Assert.IsFalse(entidad.IsValid);
        }
    }
}