using AI2.Entities;

namespace AI2.Mutation {
    public interface IMutation {
        IEnumerable<Individual> Mutate(IEnumerable<Individual> population);
    }
}
