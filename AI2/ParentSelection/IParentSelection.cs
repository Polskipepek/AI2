using AI2.Entities;

namespace AI2.ParentSelection {
    public interface IParentSelection {
        IEnumerable<Individual> GetParents(IEnumerable<Individual> population);
    }
}
