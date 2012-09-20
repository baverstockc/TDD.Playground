/****************************************************************************
 * Implement the code for a supermarket checkout that calculates the total 
 * price of a number of items. In a normal supermarket, things are identified 
 * using Stock Keeping Units, or SKUs. In our store, we’ll use individual 
 * letters of the alphabet (A, B, C, and so on). Our goods are priced 
 * individually. 
 * 
 * In addition, some items are multipriced: buy n of them, and they’ll cost 
 * you y pence. For example, item ‘A’ might cost 50 pence individually, but 
 * this week we have a special offer: buy three ‘A’s and they’ll cost you 
 * £1.30. 
 * 
 * In fact this week’s prices are:
 * 
 * Item   Unit      Special
 *        Price     Price
 * --------------------------
 *  A     50       3 for 130
 *  B     30       2 for 45
 *  C     20
 *  D     15
 * 
 * Our checkout accepts items in any order, so that if we scan a B, an A, and 
 * another B, we’ll recognize the two B’s and price them at 45 (for a total 
 * price so far of 95). Because the pricing changes frequently, we need to be 
 * able to pass in a set of pricing rules each time we start handling a 
 * checkout transaction.
 * 
 * The interface to the checkout should look like:
 * 
 * var co = new Checkout(PricingRules);
 * 
 * co.Scan(item);
 * co.Scan(item);
 *     :    :
 * var price = co.Total;
 * 
 ****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Kata.Checkout.Tests.Unit
{
    /****************************************************************************
     * Here’s a set of unit tests for a C# implementation. The helper method 
     * price lets you specify a sequence of items using a string, calling the 
     * checkout’s scan method on each item in turn before finally returning the 
     * total price.
     * 
     ****************************************************************************/
    [TestFixture]
    public class CheckoutTests
    {
        private readonly object PricingRules = null; // How you structure this is up to you!

        [Test]
        public void TestTotals()
        {
            Assert.AreEqual(0, Price(""));
            Assert.AreEqual(50, Price("A"));
            Assert.AreEqual(80, Price("AB"));
            Assert.AreEqual(115, Price("CDBA"));

            Assert.AreEqual(100, Price("AA"));
            Assert.AreEqual(130, Price("AAA"));
            Assert.AreEqual(180, Price("AAAA"));
            Assert.AreEqual(230, Price("AAAAA"));
            Assert.AreEqual(260, Price("AAAAAA"));

            Assert.AreEqual(160, Price("AAAB"));
            Assert.AreEqual(175, Price("AAABB"));
            Assert.AreEqual(190, Price("AAABBD"));
            Assert.AreEqual(190, Price("DABABA"));
        }

        [Test]
        public void TestIncremental()
        {
            var co = new Checkout(PricingRules);

            Assert.AreEqual(0, co.Total);

            co.Scan("A"); Assert.AreEqual(50, co.Total);
            co.Scan("B"); Assert.AreEqual(80, co.Total);
            co.Scan("A"); Assert.AreEqual(130, co.Total);
            co.Scan("A"); Assert.AreEqual(160, co.Total);
            co.Scan("B"); Assert.AreEqual(175, co.Total);
        }

        private decimal Price(string items)
        {
            var co = new Checkout(PricingRules);

            items
                .ToCharArray()
                .ToList()
                .ForEach(item => co.Scan(item.ToString()));

            return co.Total;
        }
    }
}
