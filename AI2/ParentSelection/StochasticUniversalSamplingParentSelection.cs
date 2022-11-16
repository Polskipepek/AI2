using AI2.Entities;
using AI2.Infrastructure;

namespace AI2.ParentSelection {
    public class StochasticUniversalSamplingParentSelection : IParentSelection {
        public StochasticUniversalSamplingParentSelection(int offspringsToKeep) {
            this.offspringsToKeep = offspringsToKeep;
        }

        private readonly int offspringsToKeep;

        public IEnumerable<Individual> GetParents(IEnumerable<Individual> population) {
            var fitnesses = GetPopulationFitness(population);

            var p = fitnesses.Sum() / population.Count();
            float start = (float)(Rand.Random.NextDouble() * p);
            IEnumerable<float> pointers = GetPointers(p, start);

            return RWS(population, pointers);
        }

        IEnumerable<float> GetPointers(float p, float start) {
            for (int i = 0; i < offspringsToKeep - 1; i++) {
                yield return start + i * p;
            }
        }

        IEnumerable<Individual> RWS(IEnumerable<Individual> population, IEnumerable<float> points) {
            for (int p = 0; p < points.Count(); p++) {
                int i = 1;
                while (GetPopulationFitness(population.Take(i)).Sum() < points.ElementAt(p)) {
                    i++;
                }
                yield return population.ElementAt(i);
            }
        }

        IEnumerable<float> GetPopulationFitness(IEnumerable<Individual> population) => population.Select(individual => individual.GetResults().wartosc);
    }
}
