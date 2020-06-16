using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class LinqShirtFinderTests : TestsBase
    {
        [Test]
        public void GivenASearchRequestForBlueShirts_WhenICallSearch_TheyAreReturned()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Medium Red", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
            };

            var shirtFinder = new LinqShirtFinder(shirts);

            var searchOptions = new SearchOptions()
            {
                Colors =
                {
                    Color.Blue
                }
            };

            var results = shirtFinder.Find(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenASearchRequestForSmallRedShirts_WhenICallSearch_TheyAreReturned()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Small Red", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
            };

            var shirtFinder = new LinqShirtFinder(shirts);

            var searchOptions = new SearchOptions()
            {
                Colors = {Color.Red},
                Sizes = {Size.Small}
            };

            var results = shirtFinder.Find(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenASearchRequestForSmallOrLargeAndRedOrBlueShirts_WhenICallSearch_TheyAreReturned()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Small Red", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Medium Blue", Size.Medium, Color.Blue),
            };

            var shirtFinder = new LinqShirtFinder(shirts);

            var searchOptions = new SearchOptions()
            {
                Colors = {Color.Red, Color.Blue},
                Sizes = {Size.Small, Size.Large}
            };

            var results = shirtFinder.Find(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenASearchRequestForSmallRedShirtsWhenNonExist_WhenICallSearch_NoneAreReturned()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Small White", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Medium Blue", Size.Medium, Color.Blue),
            };

            var shirtFinder = new LinqShirtFinder(shirts);

            var searchOptions = new SearchOptions()
            {
                Colors = {Color.Red},
                Sizes = {Size.Small}
            };

            var results = shirtFinder.Find(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenAnEmptySearchRequestWhenThereAreShirtsAvailable_WhenICallSearch_NoneAreReturned()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Small White", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Medium Blue", Size.Medium, Color.Blue),
            };

            var shirtFinder = new LinqShirtFinder(shirts);

            var searchOptions = new SearchOptions();
           
            var results = shirtFinder.Find(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenAValidSearchRequestWhenThereAreNoShirts_WhenICallSearch_NoneAreReturned()
        {
            var shirts = new List<Shirt>
            {
            };

            var shirtFinder = new LinqShirtFinder(shirts);

            var searchOptions = new SearchOptions()
            {
                Colors = { Color.Red },
                Sizes = { Size.Small }
            };

            var results = shirtFinder.Find(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void GivenANullSearchRequest_WhenICallSearch_AnExceptionIsThrown()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Small White", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Medium Blue", Size.Medium, Color.Blue),
            };

            var shirtFinder = new LinqShirtFinder(shirts);
            Assert.Throws<ArgumentNullException>(() => shirtFinder.Find(null));
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

            var shirtFinder = new LinqShirtFinder(shirts);

            var searchOptions = new SearchOptions()
            {
                Colors = { Color.Red },
                Sizes = null
            };

            Assert.Throws<ArgumentNullException>(() => shirtFinder.Find(searchOptions));
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

            var shirtFinder = new LinqShirtFinder(shirts);

            var searchOptions = new SearchOptions()
            {
                Colors = null,
                Sizes = {Size.Medium}
            };

            Assert.Throws<ArgumentNullException>(() => shirtFinder.Find(searchOptions));
        }
    }
}

