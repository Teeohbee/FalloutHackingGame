using System;
using System.Collections.Generic;
using System.Linq;

namespace FalloutHackingGame
{
    public class SelectFromChoices : IPickAChoice<int>
    {
        public int Pick(IEnumerable<int> choices)
        {
            return choices.OrderBy(x => Guid.NewGuid()).First();
        }
    }
}