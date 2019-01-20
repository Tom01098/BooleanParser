using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooleanParser.Tests
{
    [TestClass]
    public class TokenEnumeratorTests
    {
        [TestMethod]
        public void Iterate()
        {
            var tokens = new TokenEnumerator("TRUE AND NOT FALSE");

            Assert.AreEqual("TRUE", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("AND", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("NOT", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("FALSE", tokens.Current);
            Assert.AreEqual(false, tokens.MoveNext());
        }

        [TestMethod]
        public void Iterate2()
        {
            var tokens = new TokenEnumerator("(FALSE) AND NOT (TRUE OR FALSE)");

            Assert.AreEqual("(", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("FALSE", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual(")", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("AND", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("NOT", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("(", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("TRUE", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("OR", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual("FALSE", tokens.Current);
            Assert.AreEqual(true, tokens.MoveNext());
            Assert.AreEqual(")", tokens.Current);
            Assert.AreEqual(false, tokens.MoveNext());
        }
    }
}
