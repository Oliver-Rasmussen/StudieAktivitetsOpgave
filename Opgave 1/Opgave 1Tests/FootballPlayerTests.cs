using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opgave_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opgave_1.Tests
{
    [TestClass()]
    public class FootballPlayerTests
    {
        [TestMethod()]
        public void NameExceptionTest()
        {
            FootballPlayer player = new FootballPlayer();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { player.Name = "Bo"; });
        }

        [TestMethod()]
        public void NameChangeTest()
        {
            FootballPlayer player = new FootballPlayer();
            player.Name = "Karl";

            Assert.AreEqual(player.Name, "Karl");
        }

        [TestMethod()]
        public void PriceExceptionTest()
        {
            FootballPlayer player = new FootballPlayer();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { player.Price = 0; });
        }

        [TestMethod()]
        public void PriceChangeTest()
        {
            FootballPlayer player = new FootballPlayer();
            player.Price = 500;

            Assert.AreEqual(player.Price, 500);
        }

        [TestMethod()]
        public void ShirtNumberExceptionTest()
        {
            FootballPlayer player = new FootballPlayer();

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { player.ShirtNumber = 101; });
        }

        [TestMethod()]
        public void ShirtNumberChangeTest()
        {
            FootballPlayer player = new FootballPlayer();
            player.ShirtNumber = 99;

            Assert.AreEqual(player.ShirtNumber, 99);
        }
    }
}