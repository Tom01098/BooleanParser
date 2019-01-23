using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooleanParser.Tests
{
    [TestClass]
    public class ParserTests
    {
        #region Single
        [TestMethod]
        public void True()
        {
            bool result = new Parser("TRUE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void False()
        {
            bool result = new Parser("FALSE").Parse();

            Assert.AreEqual(false, result);
        }
        #endregion

        #region NOT
        [TestMethod]
        public void Not()
        {
            bool result = new Parser("NOT TRUE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Not2()
        {
            bool result = new Parser("NOT FALSE").Parse();

            Assert.AreEqual(true, result);
        }
        #endregion

        #region AND
        [TestMethod]
        public void And()
        {
            bool result = new Parser("TRUE AND TRUE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void And2()
        {
            bool result = new Parser("FALSE AND TRUE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void And3()
        {
            bool result = new Parser("TRUE AND FALSE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void And4()
        {
            bool result = new Parser("FALSE AND FALSE").Parse();

            Assert.AreEqual(false, result);
        }
        #endregion

        #region OR
        [TestMethod]
        public void Or()
        {
            bool result = new Parser("TRUE OR TRUE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Or2()
        {
            bool result = new Parser("TRUE OR FALSE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Or3()
        {
            bool result = new Parser("FALSE OR TRUE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Or4()
        {
            bool result = new Parser("FALSE OR FALSE").Parse();

            Assert.AreEqual(false, result);
        }
        #endregion

        #region XOR
        [TestMethod]
        public void Xor()
        {
            bool result = new Parser("TRUE XOR TRUE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Xor2()
        {
            bool result = new Parser("TRUE XOR FALSE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Xor3()
        {
            bool result = new Parser("FALSE XOR TRUE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Xor4()
        {
            bool result = new Parser("FALSE XOR FALSE").Parse();

            Assert.AreEqual(false, result);
        }
        #endregion

        #region NOR
        [TestMethod]
        public void Nor()
        {
            bool result = new Parser("TRUE NOR TRUE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Nor2()
        {
            bool result = new Parser("TRUE NOR FALSE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Nor3()
        {
            bool result = new Parser("FALSE NOR TRUE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Nor4()
        {
            bool result = new Parser("FALSE NOR FALSE").Parse();

            Assert.AreEqual(true, result);
        }
        #endregion

        #region NAND
        [TestMethod]
        public void Nand()
        {
            bool result = new Parser("TRUE NAND TRUE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Nand2()
        {
            bool result = new Parser("TRUE NAND FALSE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Nand3()
        {
            bool result = new Parser("FALSE NAND TRUE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Nand4()
        {
            bool result = new Parser("FALSE NAND FALSE").Parse();

            Assert.AreEqual(true, result);
        }
        #endregion

        #region XNOR
        [TestMethod]
        public void Xnor()
        {
            bool result = new Parser("TRUE XNOR TRUE").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Xnor2()
        {
            bool result = new Parser("TRUE XNOR FALSE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Xnor3()
        {
            bool result = new Parser("FALSE XNOR TRUE").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Xnor4()
        {
            bool result = new Parser("FALSE XNOR FALSE").Parse();

            Assert.AreEqual(true, result);
        }
        #endregion

        #region Multiple
        [TestMethod]
        public void Multiple()
        {
            bool result = new Parser("(TRUE)").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Multiple2()
        {
            bool result = new Parser("(TRUE AND TRUE)").Parse();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Multiple3()
        {
            bool result = new Parser("(NOT TRUE)").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Multiple4()
        {
            bool result = new Parser("NOT (TRUE OR NOT TRUE)").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Multiple5()
        {
            bool result = new Parser("(TRUE NOR FALSE) AND (FALSE OR FALSE)").Parse();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Multiple6()
        {
            bool result = new Parser("(TRUE AND TRUE) XOR NOT TRUE").Parse();

            Assert.AreEqual(true, result);
        }
        #endregion

        #region Invalid
        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void Invalid()
        {
            new Parser("(").Parse();
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void Invalid2()
        {
            new Parser("TRUE )").Parse();
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void Invalid3()
        {
            new Parser("TRUE TRUE").Parse();
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void Invalid4()
        {
            new Parser("XOR TRUE").Parse();
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void Invalid5()
        {
            new Parser("NOT AND").Parse();
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void Invalid6()
        {
            new Parser("(TRUE) AND TRUE)").Parse();
        }

        [TestMethod]
        [ExpectedException(typeof(UnexpectedTokenException))]
        public void Invalid7()
        {
            new Parser("TRUE NOT TRUE").Parse();
        }
        #endregion
    }
}
