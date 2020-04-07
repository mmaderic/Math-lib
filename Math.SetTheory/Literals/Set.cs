using Math.SetTheory.Abstractions;
using Math.SetTheory.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Math.SetTheory.Literals
{
    public class Set
    {
        protected readonly char? _identifier;
        protected readonly IEnumerable<IElement> _elements;

        public Set(char? identifier, IEnumerable<IElement> elements)
        {
            if (identifier.HasValue && (identifier < 65 || identifier > 90))
                throw new InvalidOperationException("Set identifier should be upper case letter.");

            _identifier = identifier;
            _elements = elements;
        }        

        public static implicit operator Set(string input)
            => new SetBuilder(input).ToSet();

        public override string ToString()
        {
            var identifier = _identifier.HasValue ? $"{_identifier} = " : "";

            return $"{identifier}{{{string.Join(", ", _elements.Select(x => x.ToString()))}}}";
        }
    }
}
