using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly IShirtFinder _shirtFinder;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
            _shirtFinder = new LinqShirtFinder(shirts);
        }

        public SearchResults Search(SearchOptions options)
        {
            if(options == null || options.Colors == null || options.Sizes == null)
                throw new ArgumentNullException($"{nameof(SearchOptions)} is null");

            return _shirtFinder.Find(options);
        }
    }
}