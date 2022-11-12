using AI2.Entities;

namespace AI2.Crossover {
    public interface ICrossover {
        public IEnumerable<Individual> Crossover(IEnumerable<Individual> population, IEnumerable<Individual> parents);
    }
}
