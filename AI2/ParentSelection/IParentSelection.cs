using AI2.Entities;

namespace AI2.ParentSelection {
    internal interface IParentSelection {
        IEnumerable<Individual> GetParents(IEnumerable<Individual> population);
    }
}
