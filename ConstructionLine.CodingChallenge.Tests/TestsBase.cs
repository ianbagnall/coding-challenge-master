﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class TestsBase
    {
        protected static void AssertResults(List<Shirt> shirts, SearchOptions options)
        {
            Assert.That(shirts, Is.Not.Null);

            var resultingShirtIds = shirts.Select(s => s.Id).ToList();
            var sizeIds = options.Sizes.Select(s => s.Id).ToList();
            var colorIds = options.Colors.Select(c => c.Id).ToList();

            foreach (var shirt in shirts)
            {
                if (sizeIds.Contains(shirt.Size.Id)
                    && colorIds.Contains(shirt.Color.Id)
                    && !resultingShirtIds.Contains(shirt.Id))
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }


        protected static void AssertSizeCounts(List<Shirt> shirts, SearchOptions searchOptions, List<SizeCount> sizeCounts)
        {
            Assert.That(sizeCounts, Is.Not.Null);

            foreach (var size in Size.All)
            {
                var sizeCount = sizeCounts.SingleOrDefault(s => s.Size.Id == size.Id);
                Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size.Name}' not found in results");

                var expectedSizeCount = shirts
                    .Count(s => s.Size.Id == size.Id
                                && (!searchOptions.Colors.Any() || searchOptions.Colors.Select(c => c.Id).Contains(s.Color.Id))
                                && (!searchOptions.Sizes.Any() || searchOptions.Sizes.Select(c => c.Id).Contains(s.Size.Id)));

                Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount), 
                    $"Size count for '{sizeCount.Size.Name}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
            }
        }


        protected static void AssertColorCounts(List<Shirt> shirts, SearchOptions searchOptions, List<ColorCount> colorCounts)
        {
            Assert.That(colorCounts, Is.Not.Null);
            
            foreach (var color in Color.All)
            {
                var colorCount = colorCounts.SingleOrDefault(s => s.Color.Id == color.Id);
                Assert.That(colorCount, Is.Not.Null, $"Color count for '{color.Name}' not found in results");

                var expectedColorCount = shirts
                    .Count(shirt => shirt.Color.Id == color.Id  
                                && (!searchOptions.Sizes.Any() || searchOptions.Sizes.Select(s => s.Id).Contains(shirt.Size.Id))
                                && (!searchOptions.Colors.Any() || searchOptions.Colors.Select(c => c.Id).Contains(shirt.Color.Id)));



                Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount),
                    $"Color count for '{colorCount.Color.Name}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
            }
        }

        protected static List<Shirt> CreateShirtRange()
        {
            return new List<Shirt>()
            {
                new Shirt(Guid.NewGuid(), "Small Yellow", Size.Small, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Medium Yellow", Size.Medium, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Large Yellow", Size.Large, Color.Yellow),
                new Shirt(Guid.NewGuid(), "Small White", Size.Small, Color.White),
                new Shirt(Guid.NewGuid(), "Medium White", Size.Medium, Color.White),
                new Shirt(Guid.NewGuid(), "Large White", Size.Large, Color.White),
                new Shirt(Guid.NewGuid(), "Small Red", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Medium Red", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Large Red", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "Small Blue", Size.Small, Color.Blue),
                new Shirt(Guid.NewGuid(), "Medium Blue", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Large Blue", Size.Large, Color.Blue),
                new Shirt(Guid.NewGuid(), "Small Black", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Medium Black", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Large Black", Size.Large, Color.Black),
            };
        }
    }
}