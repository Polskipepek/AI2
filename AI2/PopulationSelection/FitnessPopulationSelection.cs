using AI2.Entities;

namespace AI2.PopulationSelection {
    public class FitnessPopulationSelection : IPopulationSelection {
        public IEnumerable<Individual> GetNextPopulation(IEnumerable<Individual> oldPopulation, IEnumerable<Individual> newIndividuals) {
            return oldPopulation
                .OrderBy(x => x.GetResults().wartosc)
                .Skip(newIndividuals.Count())
                .Concat(newIndividuals);
        }
    }
}
