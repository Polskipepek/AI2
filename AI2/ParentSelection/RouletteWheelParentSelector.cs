using AI2.Entities;

namespace AI2.ParentSelection {
    public class RouletteWheelParentSelector : IParentSelection {
        public RouletteWheelParentSelector(float percentOfPopulationToSelect) {
            this.percentOfPopulationToSelect = percentOfPopulationToSelect;
        }

        private readonly float percentOfPopulationToSelect;

        public IEnumerable<Individual> GetParents(IEnumerable<Individual> population) {
            var fitnesses = GetPopulationFitness(population, out var minFitness, out var maxFitness);
            var probs = GetSelectionProbabilities(fitnesses, minFitness, maxFitness);

            var rouletteWheel = new RouletteWheel(probs);
            for (int j = 0; j < population.Count() * percentOfPopulationToSelect; j++) {
                yield return population.ElementAt(rouletteWheel.Spin());
            }
        }

        private IEnumerable<float> GetPopulationFitness(IEnumerable<Individual> population, out float minFitness, out float maxFitness) {
            var fitnesses = population.Select(individual => individual.GetResults().wartosc);

            maxFitness = fitnesses.Max();
            minFitness = fitnesses.Min();

            return fitnesses;
        }

        private float[] GetSelectionProbabilities(IEnumerable<float> populationFitness, float minFitness, float maxFitness) {
            var qPrimes = populationFitness.Select(fitness => (fitness - minFitness) / (maxFitness - minFitness));
            var qPrimesSum = qPrimes.Sum();

            var probs = qPrimes.Select(qPrime => qPrime / qPrimesSum).ToArray();

            return probs;
        }
    }
}
