using AI2.Entities;

namespace AI2.Crossover {
    internal interface ICrossover {
        public IEnumerable<Individual> Crossover(IEnumerable<Individual> population, IEnumerable<Individual> parents);
    }
}
