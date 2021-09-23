using Game.Core.Interfaces;

namespace Game.Core.Predicates
{
    public class PredicateLess : IPredicate
    {
        public bool ConditionCorrect(double val0, double val1)
        {
            return val0 < val1;
        }
    }
}
