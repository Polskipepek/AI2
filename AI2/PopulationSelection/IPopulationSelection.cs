using AI2.Entities;

namespace AI2.PopulationSelection {
    internal interface IPopulationSelection {
        IEnumerable<Individual> GetNextPopulation(IEnumerable<Individual> oldPopulation, IEnumerable<Individual> newIndividuals);
    }
}
