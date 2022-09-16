using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JGM.GameTests
{
    public class NewBehaviourScript
    {

        [OneTimeSetUp]
        public void Setup()
        {

        }

        [Test]
        public void AssertBool()
        {
            Assert.AreEqual(0, 0);
        }
    }
}