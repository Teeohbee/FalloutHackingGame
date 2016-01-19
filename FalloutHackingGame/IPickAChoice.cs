using System.Collections.Generic;

namespace FalloutHackingGame
{
    public interface IPickAChoice<T>
    {
        T Pick(IEnumerable<T> choices);
    }
}