using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class LinqShirtFinder : IShirtFinder
    {
        private readonly List<Shirt> _shirts;

        public LinqShirtFinder(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Find(SearchOptions options)
        {
            if (options == null || options.Colors == null || options.Sizes == null)
                throw new ArgumentNullException($"{nameof(SearchOptions)} is null");

            List<Shirt> selectedShirts = GetShirtsByColorAndSize(options);

            var searchResults = new SearchResults
            {
                Shirts = selectedShirts,
                SizeCounts = GetCountsOfEachSize(selectedShirts),
                ColorCounts = GetCountsOfEachColor(selectedShirts)
            };

            return searchResults;
        }

        private List<Shirt> GetShirtsByColorAndSize(SearchOptions options) =>
             _shirts.Where(shirt => (!options.Colors.Any() || options.Colors.Any(x => x == shirt.Color)) &&
                                    (!options.Sizes.Any() || options.Sizes.Any(x => x == shirt.Size))).ToList();

        private List<ColorCount> GetCountsOfEachColor(List<Shirt> selectedShirts)
        {
            var colorCounts = selectedShirts.GroupBy(shirt => shirt.Color)
                .Select(group => new ColorCount
                {
                    Color = group.Key,
                    Count = group.Count()
                }).ToList();

            GetMissingColors(colorCounts);

            return colorCounts;
        }

        private static void GetMissingColors(List<ColorCount> colorCounts) => Color.All
                .Where(color => !colorCounts.Any(x => x.Color == color)).ToList()
                .ForEach(color => colorCounts.Add(new ColorCount {Color = color, Count = 0}));

        private List<SizeCount> GetCountsOfEachSize(List<Shirt> selectedShirts)
        {
            var sizeCounts = selectedShirts.GroupBy(shirt => shirt.Size)
                .Select(group => new SizeCount
                {
                    Size = group.Key,
                    Count = group.Count()
                }).ToList();

            GetMissingSizes(sizeCounts);

            return sizeCounts;
        }

        private static void GetMissingSizes(List<SizeCount> sizeCounts) => Size.All
            .Where(size => !sizeCounts.Any(x => x.Size == size)).ToList()
            .ForEach(size => sizeCounts.Add(new SizeCount {Size = size, Count = 0}));
    }
}