using AI2.Entities;

namespace AI2.Mutation {
    internal interface IMutation {
        IEnumerable<Individual> Mutate(IEnumerable<Individual> population);
    }
}
