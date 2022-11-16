using AI2.Entities;
using AI2.Infrastructure;

namespace AI2.ParentSelection {
    public class TournamentParentSelector : IParentSelection {
        public TournamentParentSelector(float percentOfPopulationToSelect) {
            this.percentOfPopulationToSelect = percentOfPopulationToSelect;
        }

        private readonly float percentOfPopulationToSelect;

        public IEnumerable<Individual> GetParents(IEnumerable<Individual> population) {
            var fitnesses = GetPopulationFitness(population, out var _, out var _);
            var probs = GetSelectionProbabilities(fitnesses);

            Tournament tournament = new(probs);

            for (int i = 0; i < population.Count() * percentOfPopulationToSelect; i++) {
                yield return population.ElementAt(tournament.GetResult());
            }
        }

        private IEnumerable<float> GetPopulationFitness(IEnumerable<Individual> population, out float minFitness, out float maxFitness) {
            var fitnesses = population.Select(individual => individual.GetResults().wartosc);

            maxFitness = fitnesses.Max();
            minFitness = fitnesses.Min();

            return fitnesses;
        }

        private float[] GetSelectionProbabilities(IEnumerable<float> populationFitness) {
            float[] probabilites = new float[populationFitness.Count()];
            float probability = (float)(Rand.Random.NextDouble());
            probabilites[0] = probability;

            for (int i = 1; i < populationFitness.Count(); i++) {
                probabilites[i] = probability * MathF.Pow(1 - probability, i);
            }

            return probabilites;
        }
    }
}
