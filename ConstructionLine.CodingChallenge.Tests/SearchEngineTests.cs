using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : TestsBase
    {
        [Test]
        public void Test()
        {
            var shirts = CreateShirtRange();

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenANullSearchRequest_WhenICallSearch_AnExceptionIsThrown()
        {
            var shirts  = CreateShirtRange();
            
            var searchEngine = new SearchEngine(shirts);
            
            Assert.Throws<ArgumentNullException>(() => searchEngine.Search(null));
        }

        [Test]
        public void GivenANullSizeInSearchRequest_WhenICallSearch_AnExceptionIsThrown()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Small White", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Medium Blue", Size.Medium, Color.Blue),
            };

            var searchOptions = new SearchOptions()
            {
                Colors = { Color.Red },
                Sizes = null
            };

            var searchEngine = new SearchEngine(shirts);
            Assert.Throws<ArgumentNullException>(() => searchEngine.Search(null));
        }

        [Test]
        public void GivenANullColorInSearchRequest_WhenICallSearch_AnExceptionIsThrown()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Small White", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Medium Blue", Size.Medium, Color.Blue),
            };

            var searchOptions = new SearchOptions()
            {
                Colors = null,
                Sizes = { Size.Medium }
            };

            var searchEngine = new SearchEngine(shirts);
            Assert.Throws<ArgumentNullException>(() => searchEngine.Search(null));
        }
    }
}
