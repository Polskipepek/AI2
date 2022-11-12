using AI2.Entities;

namespace AI2.PopulationSelection {
    public interface IPopulationSelection {
        IEnumerable<Individual> GetNextPopulation(IEnumerable<Individual> oldPopulation, IEnumerable<Individual> newIndividuals);
    }
}
