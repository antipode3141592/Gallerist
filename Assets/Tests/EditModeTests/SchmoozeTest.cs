using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gallerist;
using NUnit.Framework;

namespace Testing.EditMode
{
    public class SchmoozeTest
    {
        Patron patron;
        Artist artist;

        [SetUp]
        public void Setup()
        {
            patron = TestFactories.GetPatron();
            artist = TestFactories.GetArtist();
        }

        [Test]
        public void ChatReturnsRevealedList()
        {
            List<ITrait> revealedTraits = Schmooze.Chat(patron: patron);
            if (revealedTraits is null)
                Assert.Fail();
            Assert.IsTrue(revealedTraits.Count > 0);
        }

        [Test]
        public void ChatFailsWhenAllTraitsRevealed()
        {
            foreach (var trait in patron.AestheticTraits)
                trait.IsKnown = true;
            foreach (var trait in patron.EmotiveTraits)
                trait.IsKnown = true;

            List<ITrait> revealedTraits = Schmooze.Chat(patron: patron);
            Assert.IsTrue(revealedTraits is null);
            Assert.IsTrue(patron.AllTraitsKnown);
        }

        [Test]
        public void NudgeReducesThresholdByOne()
        {
            Tuple<int, int> prevalue = new(patron.EmotiveThreshold, patron.AestheticThreshold);
            var result = Schmooze.Nudge(patron);
            Assert.IsTrue(prevalue.Item1 - patron.EmotiveThreshold == 1 || prevalue.Item2 - patron.AestheticThreshold == 1);
        }

        [Test]
        public void NudgeReducesThresholdByTwoWhenArtBuyer()
        {
            //bool didBuyArt = patron.BuyArt(TestFactories.GetArt());
            //Assert.IsTrue(patron.Acquisitions.Count == 1);

        }
    }
}
