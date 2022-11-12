using AI2.Entities;

namespace AI2.PopulationSelection {
    internal class FIFOPopulationSelector : IPopulationSelection {
        public IEnumerable<Individual> GetNextPopulation(IEnumerable<Individual> oldPopulation, IEnumerable<Individual> newIndividuals) {
            return oldPopulation
                .Skip(newIndividuals.Count())
                .Concat(newIndividuals);
        }
    }
}
